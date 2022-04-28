using _01_LampshadeQuery.Contract.Comment;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents {
    public class CommentViewComponent: ViewComponent {
        public IViewComponentResult Invoke (List<CommentQueryModel> comments) {

            return View(comments);
        }
    }
}
