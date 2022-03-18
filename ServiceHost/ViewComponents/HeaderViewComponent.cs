using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents {
    public class HeaderViewComponent : ViewComponent{

        public IViewComponentResult Invoke() {
            return View();
        }
    }
}
