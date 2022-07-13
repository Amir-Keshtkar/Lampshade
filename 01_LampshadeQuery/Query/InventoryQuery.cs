using _01_LampshadeQuery.Contract.Inventory;
using InventoryManagement.Infrastructure.EFCore;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query {
    public class InventoryQuery: IInventoryQuery {
        private readonly InventoryContext _inventoryContext;
        private readonly ShopContext _shopContext;

        public InventoryQuery(ShopContext shopContext, InventoryContext inventoryContext) {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
        }

        public StockStatus CheckStockStatus(IsInStock command) {
            var inventory = _inventoryContext.Inventory.FirstOrDefault(x => x.ProductId == command.ProductId);
            if(inventory == null || inventory.CalculateInventoryStock()<command.Count) {
                var product=_shopContext.Products.Select(x=> new {x.Id, x.Name}).FirstOrDefault(x=>x.Id==command.ProductId);
                return new StockStatus {
                    IsStock = false,
                    ProductName = product!.Name
                };
            }
            return new StockStatus {
                IsStock = true,
            };
        }
    }
}
