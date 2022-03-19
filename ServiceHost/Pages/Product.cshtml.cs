using _01_LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class ProductModel: PageModel {
        public ProductQueryModel product { get; set; }
        private readonly IProductQuery _productQuery;

        public ProductModel(IProductQuery productQuery) {
            _productQuery = productQuery;
        }

        public void OnGet (string id) {
            product = _productQuery.GetDetails(id);
        }
    }
}
