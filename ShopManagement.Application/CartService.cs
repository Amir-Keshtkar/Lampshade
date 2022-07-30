using ShopManagement.Application.Contract.Order;

namespace ShopManagement.Application {
    public class CartService : ICartService {
        public Cart cart { get; set; }
        public Cart Get() {
            return cart;
        }

        public void Set(Cart cart) {
            this.cart = cart;
        }
    }
}
