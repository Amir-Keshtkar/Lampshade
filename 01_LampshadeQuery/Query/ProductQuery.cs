﻿using _0_Framework.Application;
using _01_LampshadeQuery.Contract.Comment;
using _01_LampshadeQuery.Contract.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query {
    public class ProductQuery : IProductQuery {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext) {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public List<ProductQueryModel> GetLatestArrivals() {
            var products = _shopContext.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            var inventory = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId });
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId });

            foreach (var product in products) {
                var inventoryPrice = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventoryPrice == null)
                    continue;
                var price = inventoryPrice.UnitPrice;
                product.Price = price.ToMoney();

                if (productDiscount == null)
                    continue;
                var discountRate = productDiscount.DiscountRate;
                product.DiscountRate = discountRate;

                var discount = Math.Round(price * discountRate / 100);
                product.PriceWithDiscount = (price - discount).ToMoney();
                product.HasDiscount = discountRate > 0;
            }
            return products;
        }

        public ProductQueryModel GetDetails(string slug) {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.ProductId, x.UnitPrice, x.InStock });
            var discounts = _discountContext.CustomerDiscounts
                    .Where(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now)
                    .Select(x => new { x.ProductId, x.EndDate, x.DiscountRate }).ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Select(x => new ProductQueryModel {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ShortDescription = x.ShortDescription,
                    Description = x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    Slug = x.Slug,
                    Code = x.Code,
                    CategorySlug = x.Category.Slug,
                    Pictures = MapProductPictures(x.ProductPictures),
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            if (product == null) {
                return new ProductQueryModel();
            }

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            if (productInventory != null) {
                product.IsInStock = productInventory.InStock;
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();
                product.DoublePrice = price;
                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null) {
                    var discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    product.HasDiscount = discountRate > 0;
                    var discountPrice = Math.Round(price * discountRate / 100);
                    product.PriceWithDiscount = (price - discountPrice).ToMoney();
                }
            }

            product.Comments = _commentContext.Comments
               .Where(x => !x.IsCanceled && x.IsConfirmed)
               .Where(x => x.Type == CommentTypes.Product)
               .Where(x => x.RecordOwnerId == product.Id)
               .Select(x => new CommentQueryModel {
                   Id = x.Id,
                   Message = x.Message,
                   Name = x.Name,
                   CreationDate = x.CreationDate.ToFarsi()
               }).OrderByDescending(x => x.Id).ToList();

            return product;
        }

        public List<ProductQueryModel> Search(string value) {
            var inventory = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId });
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel {
                    Id = x.Id,
                    Name = x.Name,
                    Category = x.Category.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    Keywords = x.Keywords,
                    ShortDescription = x.ShortDescription
                }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(value)) {
                query = query.Where(x => x.Name.Contains(value) || x.ShortDescription!.Contains(value) || x.Keywords!.Contains(value));
            }

            var products = query.OrderByDescending(x=>x.Id).ToList();
            foreach (var product in products) {
                var inventoryPrice = inventory.FirstOrDefault(x => x.ProductId == product.Id);
                var productDiscount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (inventoryPrice == null)
                    continue;
                var price = inventoryPrice.UnitPrice;
                product.Price = price.ToMoney();

                if (productDiscount == null)
                    continue;
                var discountRate = productDiscount.DiscountRate;
                product.DiscountRate = discountRate;

                product.DiscountExpireDate = productDiscount.EndDate.ToDiscountFormat();

                var discount = Math.Round(price * discountRate / 100);
                product.PriceWithDiscount = (price - discount).ToMoney();
                product.HasDiscount = discountRate > 0;
            }
            return products;
        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems) {
            var inventory = _inventoryContext.Inventory.ToList();
            foreach (var cartItem in cartItems.Where(cartItem =>
                        inventory.Any(x => x.ProductId == cartItem.Id && x.InStock))) {
                var item = inventory.Find(x => x.ProductId == cartItem.Id && x.InStock);
                if (item != null)
                    cartItem.InStock = cartItem.Count <= item.CalculateInventoryStock();
            }
            return cartItems;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(ICollection<ProductPicture> ProductPictures) {
            return ProductPictures.Select(x => new ProductPictureQueryModel {
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId,
                IsRemoved = x.IsRemoved
            }).Where(x => !x.IsRemoved).ToList();
        }
    }
}
