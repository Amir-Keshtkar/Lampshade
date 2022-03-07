using DiscountManagement.Application.Contract.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CustomerDiscounts {
    public class IndexModel: PageModel {
        [TempData] public string Message { get; set; }
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public CustomerDiscountSearchModel? SearchModel;
        public SelectList Products { get; set; }

        public IndexModel (ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication) {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet (CustomerDiscountSearchModel searchModel) {
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate () {
            var command = new DefineCustomerDiscount {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public JsonResult OnPostCreate (DefineCustomerDiscount command) {
            var result=_customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var discount=_customerDiscountApplication.GetDetails(id);
            discount.Products = _productApplication.GetProducts();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit (EditCustomerDiscount command) {
            var result=_customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
