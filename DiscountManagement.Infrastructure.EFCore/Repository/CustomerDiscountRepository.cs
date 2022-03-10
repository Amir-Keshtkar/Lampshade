using System.Globalization;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using ShopManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EFCore.Repository {
    public class CustomerDiscountRepository: RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository {
        private readonly DiscountContext _discountContext;
        private readonly ShopContext _shopContext;

        public CustomerDiscountRepository (DiscountContext context, ShopContext shopContext) : base(context) {
            _discountContext = context;
            _shopContext = shopContext;
        }

        public EditCustomerDiscount? GetDetails (long id) {
            return _discountContext.CustomerDiscounts.Select(x => new EditCustomerDiscount {
                Id = x.Id,
                ProductId = x.ProductId,
                Reason = x.Reason,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToString(CultureInfo.InvariantCulture),
                EndDate = x.EndDate.ToString(CultureInfo.InvariantCulture)
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<CustomerDiscountViewModel> Search (CustomerDiscountSearchModel searchModel) {
            var products = _shopContext.Products.Select(x => new { x.Name, x.Id, x.CategoryId }).ToList();
            var categories = _shopContext.ProductCategories.Select(x => new { x.Name, x.Id }).ToList();
            var discounts = _discountContext.CustomerDiscounts.Select(x => new CustomerDiscountViewModel {
                Id = x.Id,
                ProductId = x.ProductId,
                DiscountRate = x.DiscountRate,
                StartDate = x.StartDate.ToFarsi(),
                EndDate = x.EndDate.ToFarsi(),
                StartDateGr = x.StartDate,
                EndDateGr = x.StartDate,
                Reason = x.Reason,
                CreationDate = x.CreationDate.ToFarsi(),
            });

            if(searchModel.ProductId > 0) {
                discounts = discounts.Where(x => x.ProductId == searchModel.ProductId);
            }
            if(!string.IsNullOrWhiteSpace(searchModel.StartDate)) {
                discounts = discounts.Where(x => x.StartDateGr >= searchModel.StartDate.ToGeorgianDateTime());
            }
            if(!string.IsNullOrWhiteSpace(searchModel.EndDate)) {
                discounts = discounts.Where(x => x.EndDateGr <= searchModel.EndDate.ToGeorgianDateTime());
            }
            var query = discounts.OrderByDescending(x => x.Id).ToList();
            if(searchModel.CategoryId > 0) {
                query.ForEach(x => x.CategoryId = products.FirstOrDefault(y => y.Id == x.ProductId)!.CategoryId);
                query = query.Where(x => x.CategoryId == searchModel.CategoryId).ToList();
            }
            query.ForEach(x => x.Product = products.FirstOrDefault(y => y.Id == x.ProductId)?.Name);
            query.ForEach(x => x.Category = categories.FirstOrDefault(y => y.Id == x.CategoryId)?.Name);
            return query;
        }
    }
}
