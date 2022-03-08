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
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<CustomerDiscountViewModel> CustomerDiscounts;
        public CustomerDiscountSearchModel? SearchModel;
        public SelectList Products { get; set; }
        public SelectList ProductCategories { get; set; }

        public IndexModel (ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication, IProductCategoryApplication productCategoryApplication) {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        public void OnGet (CustomerDiscountSearchModel searchModel) {
            CustomerDiscounts = _customerDiscountApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
            _productApplication.GetProducts().Select(x =>
                x.Id == _productCategoryApplication.GetProductCategories().FirstOrDefault(y => y.Id == x.CategoryId)!
                    .Id);
        }

        public IActionResult OnGetCreate () {
            var command = new DefineCustomerDiscount {
                Products = _productApplication.GetProducts(),
                Categories = _productCategoryApplication.GetProductCategories(),
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
            discount.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit", discount);
        }

        public JsonResult OnPostEdit (EditCustomerDiscount command) {
            var result=_customerDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
