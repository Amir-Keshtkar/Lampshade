using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository {
    public class ProductPicturesRepository: RepositoryBase<long, ProductPicture>, IProductPictureRepository {
        private readonly ShopContext _context;

        public ProductPicturesRepository (ShopContext context) : base(context) {
            _context = context;
        }

        public EditProductPicture? GetDetails (long id) {
            return _context.ProductPictures.Select(x => new EditProductPicture {
                Id = x.Id,
                //Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search (ProductPictureSearchModel searchModel) {
            var query = _context.ProductPictures
                .Include(x => x.Product)
                .Select(x => new ProductPictureViewModel {
                    Id = x.Id,
                    Picture = x.Picture,
                    CreationDate = x.CreationDate.ToFarsi(),
                    ProductId = x.ProductId,
                    IsRemoved = x.IsRemoved,
                    Product = x.Product.Name
                });
            if(searchModel.ProductId != 0) {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }

        public ProductPicture GetWithProductAndCategoryById(long id) {
            return _context.ProductPictures
                .Include(x=>x.Product)
                .ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == id)!;
        }
    }
}
