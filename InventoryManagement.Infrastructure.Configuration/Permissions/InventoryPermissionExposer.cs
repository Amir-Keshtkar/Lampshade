using _0_Framework.Infrastructure;

namespace InventoryManagement.Infrastructure.Configuration.Permissions {
    public class InventoryPermissionExposer: IPermissionExposer {
        public Dictionary<string, List<PermissionDto>> Expose () {
            return new Dictionary<string, List<PermissionDto>> {
                {
                    "Inventory", new List<PermissionDto> {
                        new PermissionDto(InventoryPermissions.ListInventories, "لیست انبار"),
                        new PermissionDto(InventoryPermissions.SearchInventories, "جستجو در انبار"),
                        new PermissionDto(InventoryPermissions.EditInventory, "ویرایش انبار"),
                        new PermissionDto(InventoryPermissions.CreateInventory, "ساخت انبار"),
                        new PermissionDto(InventoryPermissions.IncreaseInventory, "افزایش موجودی"),
                        new PermissionDto(InventoryPermissions.DecreaseInventory, "کاهش موجودی"),
                        new PermissionDto(InventoryPermissions.OperationLog, "گردش انبار"),
                    }
                },
            };
        }
    }
}
