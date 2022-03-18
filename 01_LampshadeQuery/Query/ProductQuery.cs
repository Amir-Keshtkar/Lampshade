using _0_Framework.Application;
using _01_LampshadeQuery.Contract.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EfCore;

namespace _01_LampshadeQuery.Query {
    public class ProductQuery: IProductQuery {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery (ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext) {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public List<ProductQueryModel> GetLatestArrivals () {
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
                }).OrderByDescending(x=>x.Id).Take(6).ToList();

            var inventory = _inventoryContext.Inventory.Select(x => new { x.UnitPrice, x.ProductId });
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId });

            foreach(var product in products) {
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
            return products;
        }
    }
}
