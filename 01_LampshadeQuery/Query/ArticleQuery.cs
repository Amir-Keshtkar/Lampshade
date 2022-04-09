using System.Reflection.Metadata.Ecma335;
using _0_Framework.Application;
using _01_LampshadeQuery.Contract.Article;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query {
    public class ArticleQuery: IArticleQuery {
        private readonly BlogContext _blogContext;

        public ArticleQuery (BlogContext blogContext) {
            _blogContext = blogContext;
        }

        public List<ArticleQueryModel> GetLatestArticles () {
            return _blogContext.Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate < DateTime.Now)
                .Select(x => new ArticleQueryModel {
                    Title = x.Title,
                    Slug = x.Slug,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 75)) + " ...",
                    PublishDate = x.PublishDate.ToFarsi(),
                }).Take(6).AsNoTracking().ToList();
        }

        public ArticleQueryModel GetArticleBySlug (string slug) {
            var article = _blogContext.Articles
                .Include(x => x.Category)
                .Select(x => new ArticleQueryModel {
                    Title = x.Title,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    PublishDate = x.PublishDate.ToFarsi(),
                    CategoryName = x.Category.Name,
                    CategorySlug = x.Category.Slug
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug)!;
            if(!string.IsNullOrWhiteSpace(article.Keywords)) {
                article.KeywordList = article.Keywords.Split(new char[] { ',', '،', '.' }).ToList();
            }
            return article;
        }
    }
}
