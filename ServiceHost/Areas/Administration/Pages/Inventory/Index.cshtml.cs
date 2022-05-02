using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory {
    [Authorize(Roles = Roles.Administrator)]
    public class IndexModel: PageModel {
        [TempData] public string Message { get; set; }
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;
        public List<InventoryViewModel> Inventories;
        public InventorySearchModel SearchModel;
        public SelectList Products { get; set; }

        public IndexModel (IInventoryApplication inventoryApplication, IProductApplication productApplication) {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        [NeedsPermission(InventoryPermissions.ListInventories)]
        public void OnGet (InventorySearchModel searchModel) {
            Inventories = _inventoryApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate () {
            var command = new CreateInventory() {
                Products = _productApplication.GetProducts(),
            };
            return Partial("./Create", command);
        }

        [NeedsPermission(InventoryPermissions.CreateInventory)]
        public JsonResult OnPostCreate (CreateInventory command) {
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var inventory = _inventoryApplication.GetDetails(id);
            inventory.Products = _productApplication.GetProducts();
            return Partial("./Edit", inventory);
        }

        [NeedsPermission(InventoryPermissions.EditInventory)]
        public JsonResult OnPostEdit (EditInventory command) {
            var result = _inventoryApplication.Edit(command);
            return new JsonResult(result);
        }

        public ActionResult OnGetIncrease (long id) {
            var command = new IncreaseInventory {
                InventoryId = id,
            };
            return Partial("Increase", command);
        }

        [NeedsPermission(InventoryPermissions.IncreaseInventory)]
        public IActionResult OnPostIncrease (IncreaseInventory command) {
            var result = _inventoryApplication.Increase(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetDecrease (long id) {
            var command = new DecreaseInventory() {
                InventoryId = id,
            };
            return Partial("Decrease", command);
        }
        
        [NeedsPermission(InventoryPermissions.DecreaseInventory)]
        public IActionResult OnPostDecrease (DecreaseInventory command) {
            var result = _inventoryApplication.Decrease(command);
            return new JsonResult(result);
        }
        
        [NeedsPermission(InventoryPermissions.OperationLog)]
        public IActionResult OnGetOperationLog (long id) {
            var log = _inventoryApplication.GetOperations(id);
            return Partial("OperationLog", log);
        }
    }
}
