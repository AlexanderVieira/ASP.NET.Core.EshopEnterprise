﻿namespace ESE.ShoppingCart.API.Models
{
    public class Voucher
    {
        public string Code { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? TotalDiscount { get; set; }        
        public DiscountVoucherType DiscountType { get; set; }        
        
    }
}