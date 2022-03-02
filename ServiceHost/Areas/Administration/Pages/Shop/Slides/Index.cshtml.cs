using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Application.Contract.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides {
    public class IndexModel: PageModel {
        [TempData]
        public string? Message { get; set; }
        private readonly ISlideApplication _slideApplication;
        public List<SlideViewModel>? Slides;

        public IndexModel (ISlideApplication slideApplication) {
            _slideApplication = slideApplication;
        }

        public void OnGet () {
            Slides = _slideApplication.GetAll();
        }

        public IActionResult OnGetCreate () {
            var slides = new CreateSlide();
            return Partial("./Create", slides);
        }

        public JsonResult OnPostCreate (CreateSlide command) {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var productPicture = _slideApplication.GetDetails(id);
            return Partial("./Edit", productPicture);
        }

        public JsonResult OnPostEdit (EditSlide command) {
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetRemove (long id) {
            var result = _slideApplication.Remove(id);
            if(result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore (long id) {
            var result = _slideApplication.Restore(id);
            if(result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
    }
}
