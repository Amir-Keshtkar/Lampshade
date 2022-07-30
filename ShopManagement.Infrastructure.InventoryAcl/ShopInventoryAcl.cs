using InventoryManagement.Application.Contract.Inventory;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;
using System.Linq;

namespace ShopManagement.Infrastructure.InventoryAcl
{
    public class ShopInventoryAcl : IShopInventoryAcl {
        private readonly IInventoryApplication _inventoryApplication;

        public ShopInventoryAcl(IInventoryApplication inventoryApplication) {
            _inventoryApplication = inventoryApplication;
        }

        public bool ReduceFromInventory(List<OrderItem> items) {
            var command= (from orderItem in items
                          let item = new DecreaseInventory(orderItem.ProductId, orderItem.Count, "خرید مشتری", orderItem.OrderId)
                          select item).ToList();
            return _inventoryApplication.Decrease(command).IsSucceeded;
        }
    }
}