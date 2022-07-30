using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Infrastructure.EfCore;
using ShopManagement.Application.Contract;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository {
    public class OrderRepository : RepositoryBase<long, Order>, IOrderRepository {
        private readonly ShopContext _shopContext;
        private readonly AccountContext _accountContext;
        public OrderRepository(ShopContext context, AccountContext accountContext) : base(context) {
            _shopContext = context;
            _accountContext = accountContext;
        }

        public double GetAmountBy(long id) {
            var order = _shopContext.Orders.Select(x => new { x.Id, x.PayAmount })
                .FirstOrDefault(x => x.Id == id)!.PayAmount;
            if (order != null) {
                return order;
            }
            return 0;
        }

        public List<OrderItemViewModel> GetItems(long orderId) {
            var products = _shopContext.Products.Select(x => new {x.Id, x.Name}).ToList();
            var order = _shopContext.Orders.FirstOrDefault(x => x.Id == orderId);
            if (order == null) {
                return new List<OrderItemViewModel>();
            }
            var items=order.Items.Select(x=> new OrderItemViewModel {
                Id = x.Id,
                OrderId = orderId,
                Count = x.Count,
                DiscountRate = x.DiscountRate,
                ProductId=x.ProductId,
                UnitPrice=x.UnitPrice
            }).ToList();
            foreach (var item in items) {
                item.ProductName=products.FirstOrDefault(x=>x.Id==item.ProductId)!.Name;
            }
            return items;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel) {
            var accounts = _accountContext.Accounts.Select(x => new { x.Id, x.FullName }).ToList();
            var query = _shopContext.Orders.Select(x => new OrderViewModel {
                Id = x.Id,
                AccountId = x.AccountId,
                DiscountAmount = x.DiscountAmount,
                IsCanceled = x.IsCanceled,
                IsPaid = x.IsPaid,
                IssueTrackingNo = x.IssueTrackingNo,
                PayAmount = x.PayAmount,
                PaymentMethodId = x.PaymentMethod,
                RefId = x.RefId,
                TotalAmount = x.TotalAmount,
                CreationDate = x.CreationDate.ToFarsi()
            });
            query = query.Where(x => x.IsCanceled == searchModel.IsCanceled);

            if (searchModel.AccountId > 0) {
                query = query.Where(x => x.AccountId == searchModel.AccountId);
            }

            var orders = query.OrderByDescending(x => x.Id).ToList();
            foreach (var item in orders) {
                item.AccountFullName = accounts.FirstOrDefault(x => x.Id == item.AccountId)!.FullName;
                item.PaymentMethod = PaymentMethod.GetById(item.PaymentMethodId).Name;
            }
            return orders;
        }
    }
}
