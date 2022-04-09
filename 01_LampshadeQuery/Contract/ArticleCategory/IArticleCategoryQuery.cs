
namespace _01_LampshadeQuery.Contract.ArticleCategory {
    public interface IArticleCategoryQuery {
        ArticleCategoryQueryModel GetArticleCategoryBySlug(string slug);
        List<ArticleCategoryQueryModel> GetArticleCategories();
    }
}
