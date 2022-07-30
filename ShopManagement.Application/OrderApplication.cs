using _0_Framework.Application;
using Microsoft.Extensions.Configuration;
using ShopManagement.Application.Contract.Order;
using ShopManagement.Domain.OrderAgg;
using ShopManagement.Domain.Services;

namespace ShopManagement.Application {
    public class OrderApplication : IOrderApplication {
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthHelper _authHelper;
        private readonly IConfiguration _configuration;
        private readonly IShopInventoryAcl _shopInventoryAcl;
        public OrderApplication(IOrderRepository orderRepository, IAuthHelper authHelper, IConfiguration configuration, IShopInventoryAcl shopInventoryAcl) {
            _orderRepository = orderRepository;
            _authHelper = authHelper;
            _configuration = configuration;
            _shopInventoryAcl = shopInventoryAcl;
        }

        public void Cancel(long id) {
            var order=_orderRepository.GetById(id);
            order.Cancel();
            _orderRepository.SaveChanges();
        }

        public double GetAmountBy(long id) {
            return _orderRepository.GetAmountBy(id);
        }

        public List<OrderItemViewModel> GetItems(long orderId) {
            return _orderRepository.GetItems(orderId);
        }

        public string PaymentSucceeded(long orderId, long refId) {
            var order=_orderRepository.GetById(orderId);
            order.PaymentSucceeded(refId);
            var symbole = _configuration.GetSection("Symbol").Value;
            var issueTrackingNo = CodeGenerator.Generate(symbole);
            order.SetIssueTrackingNumber(issueTrackingNo);
            if (_shopInventoryAcl.ReduceFromInventory(order.Items)) {
                _orderRepository.SaveChanges();
                return issueTrackingNo;
            }
            return "";
        }

        public long PlaceOrder(Cart cart) {
            var accountId = _authHelper.CurrentAccountId();
            var order = new Order(accountId, cart.TotalAmount, cart.DiscountAmount, cart.PayAmount, cart.PaymentMethod);

            foreach (var item in cart.Items) {
                var orderItem = new OrderItem(item.Id, item.Count, item.UnitPrice, item.DiscountRate);
                order.AddItem(orderItem);
            }

            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order.Id;
        }

        public List<OrderViewModel> Search(OrderSearchModel searchModel) {
            return _orderRepository.Search(searchModel);
        }
    }
}
