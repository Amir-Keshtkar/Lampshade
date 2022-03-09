using _0_Framework.Domain;
using InventoryManagement.Application.Inventory;

namespace InventoryManagement.Domain.InventoryAgg {
    public interface IInventoryRepository : IRepository<long, Inventory> {
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);
        Inventory GetByProductId(long productId);
    }
}
