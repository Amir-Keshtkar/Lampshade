using _01_LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;

namespace ServiceHost.Pages {
    public class CartModel: PageModel {
        public const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;
        public List<CartItem> CartItems;

        public CartModel (IProductQuery productQuery) {
            CartItems = new List<CartItem>();
            _productQuery = productQuery;
        }

        public void OnGet () {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            CartItems = _productQuery.CheckInventoryStatus(cartItems);
        }

        public IActionResult OnGetRemoveFromCart (long id) {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            Response.Cookies.Delete(CookieName);
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            if(itemToRemove != null) {
                cartItems.Remove(itemToRemove);
            }
            var options = new CookieOptions { Expires = DateTime.Now.AddDays(2), IsEssential = true, HttpOnly = false, Path = "/" };

            Response.Cookies.Append(CookieName, serializer.Serialize(cartItems), options);

            return RedirectToPage("/Cart");
        }

        public IActionResult OnGetGoToCheckOut () {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            CartItems = _productQuery.CheckInventoryStatus(cartItems);
            if (CartItems.Any(x => !x.InStock)) {
                return RedirectToPage("/Cart");
            }

            return RedirectToPage("/CheckOut");

        }
    }
}
