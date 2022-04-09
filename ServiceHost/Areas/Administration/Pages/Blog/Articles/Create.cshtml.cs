using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles {
    public class CreateModel: PageModel {
        private readonly IArticleCategoryApplication _categoryApplication;
        private readonly IArticleApplication _articleApplication;
        public SelectList ArticleCategories { get; set; }
        public CreateArticle command;
        
        public CreateModel (IArticleCategoryApplication categoryApplication, IArticleApplication articleApplication) {
            _categoryApplication = categoryApplication;
            _articleApplication = articleApplication;
        }

        public void OnGet () {
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");

        }

        public IActionResult OnPost(CreateArticle command) {
            var article = _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
