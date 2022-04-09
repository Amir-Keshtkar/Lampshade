using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles {
    public class IndexModel: PageModel {
        private readonly IArticleApplication _articleApplication;
        private readonly IArticleCategoryApplication _categoryApplication;
        public List<ArticleViewModel>? Articles;
        public ArticleSearchModel? SearchModel;
        public SelectList ArticleCategories { get; set; }

        public IndexModel (IArticleApplication articleApplication, IArticleCategoryApplication categoryApplication) {
            _articleApplication = articleApplication;
            _categoryApplication = categoryApplication;
        }


        public void OnGet (ArticleSearchModel searchModel) {
            ArticleCategories = new SelectList(_categoryApplication.GetArticleCategories(), "Id", "Name");
            Articles = _articleApplication.Search(searchModel);
        }

    }
}
