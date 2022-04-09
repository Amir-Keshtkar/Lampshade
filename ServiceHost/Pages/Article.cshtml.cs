using _01_LampshadeQuery.Contract.Article;
using _01_LampshadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class ArticleModel: PageModel {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;
        public ArticleQueryModel Article { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
         
        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery) {
            _categoryQuery = categoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet (string id) {
            Article = _articleQuery.GetArticleBySlug(id);
            ArticleCategories = _categoryQuery.GetArticleCategories();
            LatestArticles=_articleQuery.GetLatestArticles();
        }
    }
}
