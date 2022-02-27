using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories {
    public class IndexModel: PageModel {
        private readonly IProductCategoryApplication _productCategoryApplication;

        public void OnGet () {
        }
    }
}
