using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Product {
    public interface IProductApplication {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        EditProduct GetDetails(long id);
        OperationResult IsInStock(long id);
        OperationResult NotInStock(long id);
        List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
