using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contract;
using DiscountManagement.Infrastructure.EFCore;
using ShopManagement.Application.Contract.Order;

namespace _01_LampshadeQuery.Query;

public class CartCalculatorService: ICartCalculatorService {
    private readonly DiscountContext _discountContext;
    private readonly IAuthHelper _authHelper;

    public CartCalculatorService (DiscountContext discountContext, IAuthHelper authHelper) {
        _discountContext = discountContext;
        _authHelper = authHelper;
    }

    public Cart ComputeCart (List<CartItem> cartItems) {
        var cart = new Cart();
        var currentAccountRole = _authHelper.CurrentAccountRole();
        var customerDiscounts = _discountContext.CustomerDiscounts
            .Where(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now)
            .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
        var colleagueDiscounts = _discountContext.ColleagueDiscounts
            .Where(x => !x.IsRemoved)
            .Select(x => new { x.ProductId, x.DiscountRate }).ToList();
        var discount = 0;
        foreach(var cartItem in cartItems) {
            if(currentAccountRole == Roles.SystemUser) {
                var customerDiscount = customerDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                if(customerDiscount != null) {
                    discount = customerDiscount.DiscountRate;
                }
            } else {
                var colleagueDiscount = colleagueDiscounts.FirstOrDefault(x => x.ProductId == cartItem.Id);
                if(colleagueDiscount != null) {
                    discount = colleagueDiscount.DiscountRate;
                }
            }
            cartItem.DiscountRate = discount;
            cartItem.DiscountAmount = (cartItem.DiscountRate * cartItem.TotalItemPrice) / 100;
            cartItem.ItemPayAmount = cartItem.TotalItemPrice - cartItem.DiscountAmount;
            cart.Add(cartItem);
        }

        return cart;
    }
}