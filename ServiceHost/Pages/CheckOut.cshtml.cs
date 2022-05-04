using _01_LampshadeQuery.Contract;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages {
    public class CheckOutModel: PageModel {
        private readonly ICartCalculatorService _cartCalculatorService;
        public Cart Cart { get; set; }
        public const string CookieName = "cart-items";

        public CheckOutModel(ICartCalculatorService cartCalculatorService) {
            _cartCalculatorService = cartCalculatorService;
        }


        public void OnGet () {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            Cart = _cartCalculatorService.ComputeCart(cartItems);
        }
    }
}
