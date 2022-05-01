using System.Linq;
using System.Reflection.Metadata.Ecma335;
using _0_Framework.Application;
using _01_LampshadeQuery.Contract.Article;
using _01_LampshadeQuery.Contract.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Domain.CommentAgg;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_LampshadeQuery.Query {
    public class ArticleQuery: IArticleQuery {
        private readonly BlogContext _blogContext;
        private readonly CommentContext _commentContext;

        public ArticleQuery (BlogContext blogContext, CommentContext commentContext) {
            _blogContext = blogContext;
            _commentContext = commentContext;
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
                    Id = x.Id,
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

            var comments = _commentContext.Comments
               .Where(x => !x.IsCanceled && x.IsConfirmed)
               .Where(x => x.Type == CommentTypes.Article)
               .Where(x => x.RecordOwnerId == article.Id)
               .Include(x => x.Parent)
               .Include(x => x.Children)
               .Select(x => new CommentQueryModel {
                   Id = x.Id,
                   Message = x.Message,
                   Name = x.Name,
                   CreationDate = x.CreationDate.ToFarsi(),
                   ParentId = x.ParentId,
                   // ParentName = x.Parent.Name,
                   Children = MapComments(x.Children)
               }).OrderByDescending(x => x.Id).ToList();
            article.Comments = comments;
            return article;
        }

        private static List<CommentQueryModel> MapComments (List<Comment> children) {
            if(children == null || children.Count < 1) {
                return new List<CommentQueryModel>();
            }
            return children.Where(x => !x.IsCanceled && x.IsConfirmed)
                .Select(x => new CommentQueryModel {
                Name = x.Name,
                Message = x.Message,
                Id = x.Id,
                CreationDate = x.CreationDate.ToFarsi(),
                ParentId = x.ParentId,
                //ParentName = x.Parent.Name,
                Children = MapComments(x.Children)
            }).ToList();
        }
    }
}
