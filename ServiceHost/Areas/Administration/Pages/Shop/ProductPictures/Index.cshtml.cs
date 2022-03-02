using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures {
    public class IndexModel: PageModel {
        [TempData]
        public string? Message { get; set; }
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public List<ProductPictureViewModel>? ProductPictures;
        public ProductPictureSearchModel SearchModel;
        public SelectList? Products;

        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication) {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
        }


        public void OnGet (ProductPictureSearchModel searchModel) {
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate () {
            var products = new CreateProductPicture() {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", products);
        }

        public JsonResult OnPostCreate (CreateProductPicture command) {
            var result=_productPictureApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var product=_productPictureApplication.GetDetails(id);
            product.Products = _productApplication.GetProducts();
            return Partial("./Edit", product);
        }

        public JsonResult OnPostEdit (EditProductPicture command) {
            var result=_productPictureApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove(long id) {
            var result = _productPictureApplication.Remove(id);
            if (result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id) {
            var result = _productPictureApplication.Restore(id);
            if (result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
