using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Configuration {
    public class InventoryManagementBootstrapper {
        public static void Configure (IServiceCollection services, string connectionString) {
            services.AddTransient<IInventoryApplication, IInventoryApplication>();
            services.AddTransient<IInventoryRepository, IInventoryRepository>();
            services.AddDbContext<InventoryContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}