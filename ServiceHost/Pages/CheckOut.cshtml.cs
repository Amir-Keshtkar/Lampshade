using _0_Framework.Application;
using _0_Framework.Application.ZarinPal;
using _01_LampshadeQuery.Contract;
using _01_LampshadeQuery.Contract.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using ShopManagement.Application.Contract.Order;
using System.Globalization;

namespace ServiceHost.Pages {
    [Authorize]
    public class CheckOutModel : PageModel {
        private readonly IAuthHelper _authHelper;
        private readonly ICartService _cartService;
        private readonly IProductQuery _productQuery;
        private readonly IZarinPalFactory _zarinPalFactory;
        private readonly IOrderApplication _orderApplication;
        private readonly ICartCalculatorService _cartCalculatorService;
        public Cart Cart { get; set; }
        public const string CookieName = "cart-items";

        public CheckOutModel(ICartCalculatorService cartCalculatorService, ICartService cartService, IProductQuery productQuery, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory, IAuthHelper authHelper) {
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _productQuery = productQuery;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
            _authHelper = authHelper;
            Cart = new Cart();
        }

        public void OnGet() {
            var serializer = new JavaScriptSerializer();
            var value = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(value);
            Cart = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(Cart);
        }

        public IActionResult OnGetPay(string id) {
            int paymentMethod = 1;
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);

            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.InStock)) {
                return RedirectToPage("./Cart");
            }
            var mobile = _authHelper.CurrentAccountInfo().Mobile;
            var orderId = _orderApplication.PlaceOrder(cart);
            if (paymentMethod == 1) {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                                cart.PayAmount.ToString(CultureInfo.InvariantCulture),
                                mobile,
                                "",
                                "خرید از درگاه لوازم خانگی و دکوری",
                                orderId);
                return Redirect($"https://gateway.zibal.ir/start/{paymentResponse.Authority}");
            }
            else {
                var paymentResult = new PaymentResult();
                return RedirectToPage("/PaymentResult", paymentResult.Succeeded("سفارش شما با موفقیت ثبت شد", null));
            }
        }

        public IActionResult OnGetCallBack([FromQuery] string trackId, [FromQuery] string status, [FromQuery] long orderId, [FromBody] int success) {
            var orderAmount = _orderApplication.GetAmountBy(id: orderId);
            var vertificationResponse = _zarinPalFactory.CreateVerificationRequest(trackId, orderAmount.ToString());
            var result = new PaymentResult();
            if ((status == "1" || status == "2") && vertificationResponse.Status == 1) {
                var issueTrackingNo = _orderApplication.PaymentSucceeded(orderId, vertificationResponse.RefID);
                Response.Cookies.Delete(CookieName);
                result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
                return RedirectToPage("/PaymentResult", result);
            }
            else {
                result = result.Failed("پرداخت با موفقیت انجام نشد.");
                return RedirectToPage("/PaymentResult", result);
            }
        }
    }
}
