using _0_Framework.Application;
using _01_LampshadeQuery.Contract.Article;
using _01_LampshadeQuery.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query {
    public class ArticleCategoryQuery: IArticleCategoryQuery {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery (BlogContext blogContext) {
            _blogContext = blogContext;
        }

        public ArticleCategoryQueryModel GetArticleCategoryBySlug (string slug) {
            return _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Description = x.Description,
                    Slug = x.Slug,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    CanonicalAddress = x.CanonicalAddress,
                    ArticlesCount = x.Articles.Count,
                    KeywordList = MapKeywords(x.Keywords),
                    Articles = MapArticles(x.Articles)
                })
                .FirstOrDefault(x => x.Slug == slug)!;
        }

        private static List<ArticleQueryModel> MapArticles (List<Article> articles) {
            return articles.Select(x => new ArticleQueryModel {
                Title = x.Title,
                ShortDescription = x.ShortDescription.Substring(0, Math.Min(x.ShortDescription.Length, 75)) + " ...",
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                PublishDate = x.PublishDate.ToFarsi()
            }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories () {
            return _blogContext.ArticleCategories
                .Include(x => x.Articles)
                .Select(x => new ArticleCategoryQueryModel {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count
                }).ToList();
        }

        private static List<string> MapKeywords (string keywords) {
            return (!string.IsNullOrWhiteSpace(keywords)) ? 
                keywords.Split(new char[] { ',' , '،' , '.'}).ToList() : new List<string>();
        }
    }
}
