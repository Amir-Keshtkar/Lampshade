using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using ShopManagement.Infrastructure.EfCore;

namespace InventoryManagement.Infrastructure.EFCore.Repository {
    public class InventoryRepository: RepositoryBase<long, Inventory>, IInventoryRepository {
        private readonly InventoryContext _context;
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        public InventoryRepository(InventoryContext context, ShopContext shopContext, AccountContext accountContext) : base(context) {
            _context = context;
            _shopContext = shopContext;
            _accountContext = accountContext;
        }

        public EditInventory? GetDetails (long id) {
            return _context.Inventory.Select(x => new EditInventory {
                Id = x.Id,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<InventoryViewModel> Search (InventorySearchModel searchModel) {
            var products = _shopContext.Products.Select(x => new { x.Id, x.Name });
            var query = _context.Inventory.Select(x => new InventoryViewModel {
                Id = x.Id,
                InStock = x.InStock,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                CurrentCount = x.CalculateInventoryStock(),
                CreationDate = x.CreationDate.ToFarsi()
            });
            if(searchModel.InStock) {
                query = query.Where(x => !x.InStock);
            }

            if(searchModel.ProductId > 0) {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            var inventory = query.OrderByDescending(x => x.Id).ToList();
            inventory.ForEach(item => {
                item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name;
            });
            return inventory;
        }

        public Inventory GetByProductId (long productId) {
            return _context.Inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<InventoryOperationViewModel> GetOperationLog (long inventoryId) {
            var accounts=_accountContext.Accounts.Select(x=> new {x.Id, x.FullName});
            var inventory = _context.Inventory.FirstOrDefault(x => x.Id == inventoryId);
            var operations= inventory.Operations.Select(x => new InventoryOperationViewModel {
                Id = x.Id,
                Count = x.Count,
                CurrentCount = x.CurrentCount,
                Description = x.Description,
                OperationDate = x.OperationDate.ToFarsi(),
                OperatorId = x.OperatorId,
                Operation = x.Operation,
                OrderId = x.OrderId,
                InventoryId = x.InventoryId,
            }).OrderByDescending(x=>x.Id).ToList();
            foreach (var item in operations) {
                item.Operator=accounts.FirstOrDefault(x=>x.Id==item.OperatorId)?.FullName;
            }
            return operations;
        }
    }
}
