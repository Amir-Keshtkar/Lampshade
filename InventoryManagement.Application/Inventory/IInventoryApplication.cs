using _0_Framework.Application;

namespace InventoryManagement.Application.Inventory {
    public interface IInventoryApplication {
        OperationResult Create(CreateInventory command);
        OperationResult Edit(EditInventory command);
        OperationResult Increase(IncreaseInventory command);
        OperationResult Decrease(List<DecreaseInventory> command);
        OperationResult Decrease(DecreaseInventory command);
        EditInventory GetDetails(long id);
        List<InventoryViewModel> Search(InventorySearchModel searchModel);  

    }
}
