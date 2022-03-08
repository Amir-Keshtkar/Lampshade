﻿using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contract.Product;
using ShopManagement.Application.Contract.ProductCategory;

namespace DiscountManagement.Application.Contract.CustomerDiscount {
    public class DefineCustomerDiscount {
        public long ProductId { get; set; }
        public int DiscountRate { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Reason { get; set; }
        public List<ProductViewModel>?  Products { get; set; }
        public List<ProductCategoryViewModel>? Categories { get; set; }
    }
}