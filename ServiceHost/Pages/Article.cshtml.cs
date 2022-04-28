using _01_LampshadeQuery.Contract.Article;
using _01_LampshadeQuery.Contract.ArticleCategory;
using CommentManagement.Application.Contract.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages {
    public class ArticleModel: PageModel {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _categoryQuery;
        private readonly ICommentApplication _commentApplication;
        public ArticleQueryModel Article { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }

        public ArticleModel (IArticleQuery articleQuery, IArticleCategoryQuery categoryQuery, ICommentApplication commentApplication) {
            _categoryQuery = categoryQuery;
            _commentApplication = commentApplication;
            _articleQuery = articleQuery;
        }

        public void OnGet (string id) {
            Article = _articleQuery.GetArticleBySlug(id);
            ArticleCategories = _categoryQuery.GetArticleCategories();
            LatestArticles = _articleQuery.GetLatestArticles();
        }

        public RedirectToPageResult OnPost (AddComment command, string articleSlug) {
            command.Type = CommentTypes.Article;
            var result = _commentApplication.Add(command);
            return RedirectToPage("/Article", new { Id = articleSlug });
        }
    }
}
