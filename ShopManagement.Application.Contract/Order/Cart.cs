﻿namespace ShopManagement.Application.Contract.Order;

public class Cart {
    public double TotalAmount { get; set; }
    public double DiscountAmount { get; set; }
    public double PayAmount { get; set; }
    public int PaymentMethod { get; set; }
    public List<CartItem> Items { get; set; }

    public Cart() {
        Items = new List<CartItem>();
    }

    public void SetPaymentMethod(int methodId) {
        this.PaymentMethod=methodId;
    }
    public void Add(CartItem item) {
        Items.Add(item);
        TotalAmount += item.TotalItemPrice;
        DiscountAmount += item.DiscountAmount;
        PayAmount += item.ItemPayAmount;
    }
}