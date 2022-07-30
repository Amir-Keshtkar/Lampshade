using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contract;
using _01_LampshadeQuery.Contract.Product;
using _01_LampshadeQuery.Contract.ProductCategory;
using _01_LampshadeQuery.Contract.Slide;
using _01_LampshadeQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Application.Contract.Slide;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.Services;
using ShopManagement.Domain.SlideAgg;
using ShopManagement.Infrastructure.Configuration.Permissions;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;
using ShopManagement.Infrastructure.InventoryAcl;

namespace ShopManagement.Infrastructure.Configuration {
    public class ShopManagementBootstrapper {

        public static void Configure(IServiceCollection services, string connectionString) {
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureRepository, ProductPicturesRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddTransient<ISlideRepository, SlideRepository>();
            services.AddTransient<ISlideApplication, SlideApplication>();

            services.AddTransient<ISlideQuery, SlideQuery>();
            services.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            services.AddTransient<IProductQuery, ProductQuery>();

            services.AddTransient<IPermissionExposer, ShopPermissionExposer>();
            services.AddTransient<ICartCalculatorService, CartCalculatorService>();

            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderApplication, OrderApplication>();

            services.AddSingleton<ICartService, CartService>();
            services.AddTransient<IShopInventoryAcl, ShopInventoryAcl>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }

    }
}