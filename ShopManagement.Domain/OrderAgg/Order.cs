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
        public List<OrederItem> Items { get; private set; }

        public Order(long accountId, double totalAmount, double discountAmount, double payAmount,
            string issueTrackingNo, List<OrederItem> items) {
            AccountId = accountId;
            TotalAmount = totalAmount;
            DiscountAmount = discountAmount;
            PayAmount = payAmount;
            IssueTrackingNo = issueTrackingNo;
            RefId = 0;
            IsPaid = false;
            IsCanceled = false;
            Items = items;
        }
        public void PaymentSucceeded(long refId) {
            if (refId != 0) {
                IsPaid=true;
                RefId=refId;
            }
        }
        public void SetIssueTrackingNumber(string number) {
            IssueTrackingNo=number;
        }
        public void Cancel() {
            IsCanceled=true;
        }

        public void AddItem(OrederItem item) {
            Items.Add(item);
        }
    }
}
