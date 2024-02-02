using _0_Framework.Domain;

namespace ShopManagement.Domain.OrderAgg {
    public class Order : EntityBase {
        public long AccountId { get; private set; }
        public double TotalAmount { get; private set; }
        public double DiscountAmount { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public string IssueTrackingNo { get; private set; }
        public long RefId { get; private set; }
        public int PaymentMethod { get; private set; }
        public List<OrderItem> Items { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount, double payAmount, int paymentMethod) {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            RefId = 0;
            IsPaid = false;
            IsCanceled = false;
            Items = new List<OrderItem>();
            PaymentMethod = paymentMethod;
            IssueTrackingNo = "";
        }
        public void PaymentSucceeded(long refId) {
            IsPaid = true;
            if (refId != 0) {
                RefId = refId;
            }
        }
        public void SetIssueTrackingNumber(string number) {
            IssueTrackingNo = number;
        }
        public void Cancel() {
            IsCanceled = true;
        }

        public void AddItem(OrderItem item) {
            Items.Add(item);
        }
    }
}
