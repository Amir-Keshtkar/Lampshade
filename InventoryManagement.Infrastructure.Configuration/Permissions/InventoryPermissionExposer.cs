using _0_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permissions {
    public class InventoryPermissionExposer: IPermissionExposer {
        public Dictionary<string, List<PermissionDto>> Expose () {
            return new Dictionary<string, List<PermissionDto>> {
                {
                    "Inventory", new List<PermissionDto> {
                        new PermissionDto(InventoryPermissions.ListInventories, "ListInventories"),
                        new PermissionDto(InventoryPermissions.SearchInventories, "SearchInventories"),
                        new PermissionDto(InventoryPermissions.EditInventory, "EditInventory"),
                        new PermissionDto(InventoryPermissions.CreateInventory, "CreateInventory"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory, "IncreaseInventory"),
                        new PermissionDto(InventoryPermissions.DecreaseInventory, "DecreaseInventory"),
                        new PermissionDto(InventoryPermissions.OperationLog, "OperationLog"),
                    }
                },
            };
        }
    }
}
