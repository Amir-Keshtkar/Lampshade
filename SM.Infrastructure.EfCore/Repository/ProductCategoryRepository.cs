﻿using _0_Framework.Application;
using ShopManagement.Application.Contract.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using _0_Framework.Infrastructure;

namespace ShopManagement.Infrastructure.EfCore.Repository {
    public class ProductCategoryRepository: RepositoryBase<long, ProductCategory>, IProductCategoryRepository {
        private readonly ShopContext _shopContext;

        public ProductCategoryRepository (ShopContext shopContext) : base(shopContext) {
            _shopContext = shopContext;
        }

        public EditProductCategory GetDetails (long id) {
            var productCategory = _shopContext.ProductCategories.Select(x => new EditProductCategory {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                // Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);

            return productCategory;
        }

        public List<ProductCategoryViewModel> Search (ProductCategorySearchModel searchModel) {
            var productCategory = _shopContext.ProductCategories.Select(x => new ProductCategoryViewModel {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                CreationDate = x.CreationDate.ToFarsi(),
            });
            if(!string.IsNullOrWhiteSpace(searchModel.Name)) {
                productCategory = productCategory.Where(x => x.Name.Contains(searchModel.Name));
            }

            return productCategory.OrderByDescending(x => x.Id).ToList();
        }

        public List<ProductCategoryViewModel> GetProductCategories() {
            return _shopContext.ProductCategories.Select(x=>new ProductCategoryViewModel {
                Id=x.Id,
                Name = x.Name
            }).ToList();
        }

        public string GetSlugById(long id) {
            return _shopContext.ProductCategories.Select(x => new { x.Id, x.Slug }).FirstOrDefault(x => x.Id == id).Slug;
        }
    }
}