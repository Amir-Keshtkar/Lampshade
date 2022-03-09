using System.Security.Cryptography.X509Certificates;
using _0_Framework.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;

namespace InventoryManagement.Application {
    public class InventoryApplication: IInventoryApplication {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryApplication (IInventoryRepository inventoryRepository) {
            _inventoryRepository = inventoryRepository;
        }

        public OperationResult Create (CreateInventory command) {
            var operation = new OperationResult();
            if(_inventoryRepository.Exists(x => x.ProductId == command.ProductId)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            var inventory = new Inventory(command.ProductId, command.UnitPrice);
            _inventoryRepository.Create(inventory);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Edit (EditInventory command) {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetById(command.Id);
            if(inventory == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            if(_inventoryRepository.Exists(x => x.ProductId == command.ProductId && x.Id != command.Id)) {
                return operation.Failed(ApplicationMessages.DuplicatedMessage);
            }
            inventory.Edit(command.ProductId, command.UnitPrice);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Increase (IncreaseInventory command) {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetById(command.InventoryId);
            if(inventory == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            var operatorId = 1;
            inventory.Increase(command.Count, operatorId, command.Description);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public OperationResult Decrease (List<DecreaseInventory> command) {
            var operation=new OperationResult();
            var operatorId = 1;
            foreach(var item in command) {
                var inventory = _inventoryRepository.GetByProductId(item.ProductId);
                inventory.Decrease(item.Count,operatorId,item.Description,item.OrderId);
            }
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();   
        }

        public OperationResult Decrease (DecreaseInventory command) {
            var operation = new OperationResult();
            var inventory = _inventoryRepository.GetById(command.InventoryId);
            if(inventory == null) {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }

            var operatorId = 1;
            inventory.Decrease(command.Count, operatorId, command.Description, 0);
            _inventoryRepository.SaveChanges();
            return operation.Succeeded();
        }

        public EditInventory GetDetails (long id) {
            return _inventoryRepository.GetDetails(id);
        }

        public List<InventoryViewModel> Search (InventorySearchModel searchModel) {
            return _inventoryRepository.Search(searchModel);
        }
    }
}
