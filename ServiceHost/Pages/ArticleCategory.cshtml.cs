using _01_LampshadeQuery.Contract.Article;
using _01_LampshadeQuery.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class ArticleCategoryModel: PageModel {
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly IArticleQuery _articleQuery;

        public ArticleCategoryQueryModel ArticleCategory { get; set; }
        public List<ArticleQueryModel> Articles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

        public ArticleCategoryModel (IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery) {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet (string id) {
            ArticleCategory = _articleCategoryQuery.GetArticleCategoryBySlug(id);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();
            Articles = _articleQuery.GetLatestArticles();
        }
    }
}
