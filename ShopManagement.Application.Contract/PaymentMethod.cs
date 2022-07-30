namespace ShopManagement.Application.Contract {
    public class PaymentMethod {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private PaymentMethod(int id, string name, string description) {
            Id = id;
            Name = name;
            Description = description;
        }
        public static List<PaymentMethod> GetList() {
            return new List<PaymentMethod> {
               new PaymentMethod(1, "پرداخت اینترنتی",
                    "در مدل شما به درگاه پرداخت اینترنتی هدایت شده و درلحظه پرداخت وجه را انجام خواهید داد."),
                new PaymentMethod(2, "پرداخت نقدی",
                    "در این مدل، ابتدا سفارش ثبت شده و سپس وجه به صورت نقدی در زمان تحویل کالا، دریافت خواهد شد.")
            };
        }
        public static PaymentMethod GetById(int id) {
            return GetList().FirstOrDefault(x => x.Id == id)!;
        }
    }
}
