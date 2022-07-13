using _01_LampshadeQuery.Contract.Inventory;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Presentation.Api {
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase {
        private readonly IInventoryApplication _inventoryApplication;
        private readonly IInventoryQuery _inventoryQuery;

        public InventoryController(IInventoryApplication inventoryApplication, IInventoryQuery inventoryQuery) {
            _inventoryApplication = inventoryApplication;
            _inventoryQuery = inventoryQuery;
        }

        [HttpGet("GetOperationsBy/{id:int}")]
        public List<InventoryOperationViewModel> GetOperationsBy(long id) {
            return _inventoryApplication.GetOperations(id);
        }

        [HttpPost]
        public StockStatus CheckStockStatus(IsInStock command) {
            return _inventoryQuery.CheckStockStatus(command);
        }
    }
}
