using _01_LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class SearchModel: PageModel {
        private readonly IProductQuery _product;
        public List<ProductQueryModel> Products;
        public string Value;

        public SearchModel(IProductQuery product) {
            _product = product;
        }

        public void OnGet (string value) {
            Value = value;
            Products = _product.Search(value);
        }
    }
}
