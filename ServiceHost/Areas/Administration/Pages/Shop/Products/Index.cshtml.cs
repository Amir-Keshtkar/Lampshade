using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products {
    public class IndexModel: PageModel {
        [TempData]
        public string? Message { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public List<ProductViewModel>? Products;
        public ProductSearchModel? SearchModel;
        public SelectList? ProductCategories;

        public IndexModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication) {
            _productApplication = productApplication;
            _productCategoryApplication = productCategoryApplication;
        }


        public void OnGet (ProductSearchModel searchModel) {
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(),"Id","Name");
            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate () {
            var categories = new CreateProduct {
                ProductCategories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", categories);
        }

        public JsonResult OnPostCreate (CreateProduct command) {
            var result=_productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var product=_productApplication.GetDetails(id);
            product.ProductCategories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit (EditProduct command) {
            var result=_productApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetIsInStock(long id) {
            var result = _productApplication.IsInStock(id);
            if (result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetNotInStock(long id) {
            var result = _productApplication.NotInStock(id);
            if (result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
