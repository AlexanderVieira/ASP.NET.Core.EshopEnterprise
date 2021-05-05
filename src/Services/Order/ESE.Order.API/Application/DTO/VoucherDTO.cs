namespace ESE.Order.API.Application.DTO
{
    public class VoucherDTO
    {
        public decimal? Percentage { get; set; }
        public decimal? TotalDiscount { get; set; }
        public string Code { get; set; }
        public int DiscountType { get; set; }
    }
}