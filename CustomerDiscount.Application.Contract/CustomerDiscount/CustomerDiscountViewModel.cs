﻿namespace DiscountManagement.Application.Contract.CustomerDiscount;

public class CustomerDiscountViewModel {
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string? Product { get; set; }
    public int DiscountRate { get; set; }
    public string? StartDate { get; set; }
    public string? EndDate { get; set; }
    public DateTime StartDateGr { get; set; }
    public DateTime EndDateGr { get; set; }
    public string? Reason { get; set; }
    public string? CreationDate { get; set; }
    public long CategoryId { get; set; }
    public string? Category { get; set; }

}