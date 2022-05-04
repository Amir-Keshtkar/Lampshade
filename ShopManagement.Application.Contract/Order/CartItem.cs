namespace ShopManagement.Application.Contract.Order {
    public class CartItem {
        public long Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public string Picture { get; set; }
        public int Count { get; set; }
        public bool InStock { get; set; }
        public int DiscountRate { get; set; }
        public double DiscountAmount { get; set; }
        public double ItemPayAmount { get; set; }
        public double TotalItemPrice {
            get => UnitPrice * Count;
            // set => value= UnitPrice * Count;
        }

        public CartItem () {
            // string unitPrice=Regex.Replace(UnitPrice, @"[,]", string.Empty);
            // TotalItemPrice = UnitPrice * Count;

        }
    }
}
