using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Infrastructure.Configuration.Permissions;

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

        [NeedsPermission(ShopPermissions.ListProduct)]
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

        [NeedsPermission(ShopPermissions.CreateProduct)]
        public JsonResult OnPostCreate (CreateProduct command) {
            var result=_productApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var product=_productApplication.GetDetails(id);
            product.ProductCategories = _productCategoryApplication.GetProductCategories();
            return Partial("./Edit", product);
        }

        [NeedsPermission(ShopPermissions.EditProduct)]
        public JsonResult OnPostEdit (EditProduct command) {
            var result=_productApplication.Edit(command);
            return new JsonResult(result);
        }
    }
}
