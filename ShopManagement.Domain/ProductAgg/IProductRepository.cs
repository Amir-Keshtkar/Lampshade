using _0_Framework.Domain;
using ShopManagement.Application.Contract.Product;

namespace ShopManagement.Domain.ProductAgg {
    public interface IProductRepository: IRepository<long, Product> {
        List<ProductViewModel> Search (ProductSearchModel searchModel);
        EditProduct GetDetails (long id);
        Product GetProductWithCategoryById (long id);
    }

}
