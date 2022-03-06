using _01_LampshadeQuery.Contract.ProductCategory;
using SM.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query {
    public class ProductCategoryQuery : IProductCategoryQuery{
        private readonly ShopContext _shopContext;

        public ProductCategoryQuery(ShopContext shopContext) {
            _shopContext = shopContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories() {
            return _shopContext.ProductCategories.Select(x=>new ProductCategoryQueryModel {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).ToList();
        }
    }
}
