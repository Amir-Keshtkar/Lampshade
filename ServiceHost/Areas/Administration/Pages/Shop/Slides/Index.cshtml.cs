using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contract.Slide;
using ShopManagement.Infrastructure.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides {
    public class IndexModel: PageModel {
        [TempData]
        public string? Message { get; set; }
        private readonly ISlideApplication _slideApplication;
        public List<SlideViewModel>? Slides;

        public IndexModel (ISlideApplication slideApplication) {
            _slideApplication = slideApplication;
        }

        [NeedsPermission(ShopPermissions.ListSlides)]
        public void OnGet () {
            Slides = _slideApplication.GetAll();
        }

        public IActionResult OnGetCreate () {
            var slides = new CreateSlide();
            return Partial("./Create", slides);
        }
        
        [NeedsPermission(ShopPermissions.CreateSlide)]
        public JsonResult OnPostCreate (CreateSlide command) {
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }

        public IActionResult OnGetEdit (long id) {
            var productPicture = _slideApplication.GetDetails(id);
            return Partial("./Edit", productPicture);
        }
        
        [NeedsPermission(ShopPermissions.EditSlide)]
        public JsonResult OnPostEdit (EditSlide command) {
            var result = _slideApplication.Edit(command);
            return new JsonResult(result);
        }
        
        [NeedsPermission(ShopPermissions.RemoveSlide)]
        public IActionResult OnGetRemove (long id) {
            var result = _slideApplication.Remove(id);
            if(result.IsSucceeded) {
                return RedirectToPage("./Index");
            }
            Message = result.Message;
            return RedirectToPage("./Index");
        }
        
        [NeedsPermission(ShopPermissions.RemoveSlide)]
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
