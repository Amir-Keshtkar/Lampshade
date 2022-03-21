using _0_Framework.Domain;
using ShopManagement.Application.Contract.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg {
    public interface IProductPictureRepository: IRepository<long, ProductPicture> {
        EditProductPicture? GetDetails(long id);
        List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
        ProductPicture GetWithProductAndCategoryById(long id);
    }
}
