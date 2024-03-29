using System.Text.Encodings.Web;
using System.Text.Unicode;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShopManagement.Infrastructure.Configuration;
using ServiceHost;
using ShopManagement.Presentation.Api;
using InventoryManagement.Presentation.Api;
using _0_Framework.Application.ZarinPal;
using DNTCaptcha.Core;
using AccountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;
using InventoryManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using BlogManagement.Infrastructure.EFCore;

var builder = WebApplication.CreateBuilder(args);

// Create services to the container.
builder.Services.AddHttpContextAccessor();
var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");
ShopManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

builder.Services.Configure<CookiePolicyOptions>(options => {
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("AdminArea", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

    options.AddPolicy("Shop", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Discount", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Inventory", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Account", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator }));

    options.AddPolicy("Blog", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));

    options.AddPolicy("Comments", policyBuilder =>
        policyBuilder.RequireRole(new List<string> { Roles.Administrator, Roles.ContentUploader }));
});

builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options => {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "Inventory");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Blog", "Blog");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Comments", "Comments");

    }).AddApplicationPart(typeof(ProductController).Assembly)
    .AddApplicationPart(typeof(InventoryController).Assembly)
    .AddNewtonsoftJson();

builder.Services.AddDNTCaptcha(options => {
    options.UseCookieStorageProvider(SameSiteMode.None)
    //.UseCustomFont(Path.Combine(_env.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf"))
    .AbsoluteExpiration(minutes: 7)
    .ShowThousandsSeparators(false)
    .WithNoise(pixelsDensity: 250, linesCount: 3)
    .WithEncryptionKey("Secure Key Validator")
    .InputNames(
        new DNTCaptchaComponent {
            CaptchaHiddenInputName = "DNTCaptchaText",
            CaptchaHiddenTokenName = "DNTCaptchaToken",
            CaptchaInputName = "DNTCaptchaInputText"
        })
    .Identifier("dntCaptcha");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();
app.InitializeDatabase();
app.MapRazorPages();
app.MapControllers();

app.Run();

public static class DependencyInjection {
    public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app) {
        using (var scope = app.ApplicationServices.CreateScope()) {

            var accountContext = scope.ServiceProvider.GetRequiredService<AccountContext>();
            accountContext.Database.Migrate();

            var shopContext = scope.ServiceProvider.GetRequiredService<ShopContext>();
            shopContext.Database.Migrate();

            var discountContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            discountContext.Database.Migrate();

            var inventoryContext = scope.ServiceProvider.GetRequiredService<InventoryContext>();
            inventoryContext.Database.Migrate();


            var blogContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            blogContext.Database.Migrate();


            var commentContext = scope.ServiceProvider.GetRequiredService<CommentContext>();
            commentContext.Database.Migrate();
        }
        return app;
    }
}