using _01_LampshadeQuery.Contract.Product;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class ProductModel: PageModel {
        public ProductQueryModel product { get; set; }
        private readonly IProductQuery _productQuery;
        private readonly ICommentApplication _commentApplication;

        public ProductModel(IProductQuery productQuery, ICommentApplication commentApplication) {
            _productQuery = productQuery;
            _commentApplication = commentApplication;
        }

        public void OnGet (string id) {
            product = _productQuery.GetDetails(id);
        }

        public RedirectToPageResult OnPost(AddComment command, string productSlug) {
            command.Type = CommentTypes.Product;
            var result=_commentApplication.Add(command);
            return RedirectToPage("/Product", new{Id=productSlug});
        }
    }
}
