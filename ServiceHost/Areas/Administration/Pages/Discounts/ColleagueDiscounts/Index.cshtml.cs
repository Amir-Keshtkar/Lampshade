using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Infrastructure.Configuration.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.ColleagueDiscounts {
    public class IndexModel: PageModel {
        [TempData] public string Message { get; set; }
        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<ColleagueDiscountViewModel> ColleagueDiscounts;
        public ColleagueDiscountSearchModel? SearchModel;
        public SelectList Products { get; set; }

        public IndexModel (IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication, IProductCategoryApplication productCategoryApplication) {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }

        [NeedsPermission(DiscountPermissions.ListColleagueDiscounts)]
        public void OnGet (ColleagueDiscountSearchModel searchModel) {
            ColleagueDiscounts = _colleagueDiscountApplication.Search(searchModel);
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public IActionResult OnGetCreate () {
            var command = new DefineColleagueDiscount{
                Products = _productApplication.GetProducts(),
                Categories = _productCategoryApplication.GetProductCategories(),
            };
            return Partial("./Create", command);
        }
        
        [NeedsPermission(DiscountPermissions.CreateColleagueDiscounts)]
        public JsonResult OnPostCreate (DefineColleagueDiscount command) {
            var result=_colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var discount=_colleagueDiscountApplication.GetDetails(id);
            discount.Products = _productApplication.GetProducts();
            discount.Categories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit", discount);
        }
        
        [NeedsPermission(DiscountPermissions.EditColleagueDiscounts)]
        public JsonResult OnPostEdit (EditColleagueDiscount command) {
            var result=_colleagueDiscountApplication.Edit(command);
            return new JsonResult(result);
        }
        
        [NeedsPermission(DiscountPermissions.RemoveColleagueDiscounts)]
        public IActionResult OnGetRemove(long id) {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }

        [NeedsPermission(DiscountPermissions.RemoveColleagueDiscounts)]
        public IActionResult OnGetRestore(long id) {
            _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
