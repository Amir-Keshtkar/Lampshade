namespace _01_LampshadeQuery.Contract.Article;

public interface IArticleQuery {
    List<ArticleQueryModel> GetLatestArticles();
    ArticleQueryModel GetArticleBySlug(string slug);
}