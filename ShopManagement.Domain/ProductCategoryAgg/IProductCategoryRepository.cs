using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg {
    public interface IProductCategoryRepository : IRepository<long , ProductCategory> {
        EditProductCategory GetDetails(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
        List<ProductCategoryViewModel> GetProductCategories ();
        string GetSlugById(long id);
    }
}
