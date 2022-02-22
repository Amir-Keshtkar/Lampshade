
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using SM.Infrastructure.EfCore;
using SM.Infrastructure.EfCore.Repository;

namespace ShopManagement.Configuration {
    public class ShopManagementBootstrapper {

        public static void Configure(IServiceCollection services, string connectionString) {
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();

            services.AddDbContext<ShopContext>(x => x.UseSqlServer(connectionString));
        }

    }
}  