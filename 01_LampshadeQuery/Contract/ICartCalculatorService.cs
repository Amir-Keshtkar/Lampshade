using ShopManagement.Application.Contract.Order;

namespace _01_LampshadeQuery.Contract {
    public interface ICartCalculatorService {
        Cart ComputeCart (List<CartItem> cartItems);
    }
}
