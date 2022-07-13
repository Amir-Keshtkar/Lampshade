using _01_LampshadeQuery.Contract.Article;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Presentation.Api.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController: ControllerBase {
        private readonly IArticleQuery _articleQuery;

        public BlogController(IArticleQuery articleQuery) {
            _articleQuery = articleQuery;
        }

        [HttpGet("GetLatestArticles")]
        public List<ArticleQueryModel> GetLatestArticles() {
            return _articleQuery.GetLatestArticles();
        }
    }
}