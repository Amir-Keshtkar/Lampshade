using System.Globalization;
using _0_Framework.Application;
using _01_LampshadeQuery.Contract.Product;
using _01_LampshadeQuery.Contract.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query {
    public class ProductCategoryQuery: IProductCategoryQuery {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery (ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext) {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductCategoryQueryModel> GetProductCategories () {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel {
                Id = x.Id,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).AsNoTracking().ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts () {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId });
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();

            var categories = _shopContext.ProductCategories
                .Include(x => x.Products)!
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products!)
                }).AsNoTracking().ToList();

            foreach(var product in categories.SelectMany(category => category.Products)) {
                var inventoryPrice = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if(inventoryPrice == null)
                    continue;
                var price = inventoryPrice.UnitPrice;
                product.Price = price.ToMoney();

                if(productDiscount == null)
                    continue;
                var discountRate = productDiscount.DiscountRate;
                product.DiscountRate = discountRate;

                var discount = Math.Round(price * discountRate / 100);
                product.PriceWithDiscount = (price - discount).ToMoney();
                product.HasDiscount = discountRate > 0;
            }
            return categories;
        }

        private static List<ProductQueryModel> MapProducts (List<Product> products) {
            return products.Select(product => new ProductQueryModel {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
            }).ToList();
        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBySlug (string slug) {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId });
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var category = _shopContext.ProductCategories
                .Include(x => x.Products)!
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    Slug = x.Slug,
                    Products = MapProducts(x.Products!)
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach(var product in category.Products) {
                var inventoryPrice = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if(inventoryPrice == null)
                    continue;
                var price = inventoryPrice.UnitPrice;
                product.Price = price.ToMoney();

                if(productDiscount == null)
                    continue;
                var discountRate = productDiscount.DiscountRate;
                product.DiscountRate = discountRate;

                product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();

                var discount = Math.Round(price * discountRate / 100);
                product.PriceWithDiscount = (price - discount).ToMoney();
                product.HasDiscount = discountRate > 0;
            }
            return category;
        }
    }
}
