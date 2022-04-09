using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.Infrastructure.EFCore.Repository {
    public class ArticleRepository: RepositoryBase<long, Article>, IArticleRepository {
        private readonly BlogContext _blogContext;

        public ArticleRepository (BlogContext context) : base(context) {
            _blogContext = context;
        }


        public EditArticle GetDetails (long id) {
            return _blogContext.Articles.Select(x => new EditArticle {
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.Description,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                CanonicalAddress = x.CanonicalAddress,
                PublishDate = x.PublishDate.ToFarsi(),
                CategoryId = x.CategoryId,
                Id = x.Id,
            }).FirstOrDefault(x => x.Id == id)!;
        }

        public Article GetWithCategoryById(long id) {
            return _blogContext.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id)!;
        }

        public List<ArticleViewModel> Search (ArticleSearchModel searchModel) {
            var query = _blogContext.Articles.Select(x => new ArticleViewModel {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,50))+" ...",
                Picture = x.Picture,
                PublishDate = x.PublishDate.ToFarsi(),
                Category = x.Category.Name,
                CategoryId = x.CategoryId
            });
            if(!string.IsNullOrWhiteSpace(searchModel.Title)) {
                query = query.Where(x => x.Title.Contains(searchModel.Title));
            }

            if(searchModel.CategoryId > 0) {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
