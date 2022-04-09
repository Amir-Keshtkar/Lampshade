using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles {
    public class EditModel: PageModel {
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly IArticleApplication _articleApplication;
        public SelectList ArticleCategories { get; set; }
        [BindProperty] public EditArticle command { get; set; }

        public EditModel (IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication) {
            _categoryApplication = categoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet (long id) {
            command = _articleApplication.GetDetails(id);
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
        }

        public IActionResult OnPost (EditArticle command) {
            var article = _articleApplication.Edit(command);
            return RedirectToPage("./Index");
        }
    }
}
