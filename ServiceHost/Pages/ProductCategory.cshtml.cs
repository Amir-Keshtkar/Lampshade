using _01_LampshadeQuery.Contract.Product;
using _01_LampshadeQuery.Contract.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class ProductCategoryModel: PageModel {
        private readonly IProductCategoryQuery _productCategory;
        public ProductCategoryQueryModel Category;
        public ProductCategoryModel(IProductCategoryQuery productCategory) {
            _productCategory = productCategory;
        }

        public void OnGet (string id) {
            Category = _productCategory.GetProductCategoryWithProductsBySlug(id);

        }
    }
}
