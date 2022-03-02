
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using SM.Infrastructure.EfCore;
using SM.Infrastructure.EfCore.Repository;

namespace ShopManagement.Configuration {
    public class ShopManagementBootstrapper {

        public static void Configure (IServiceCollection services, string connectionString) {
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddTransient<IProductApplication, ProductApplication>();
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductPictureRepository, ProductPicturesRepository>();
            services.AddTransient<IProductPictureApplication, ProductPictureApplication>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }

    }
}