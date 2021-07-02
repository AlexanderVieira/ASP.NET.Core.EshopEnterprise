namespace ESE.Porchase.API.Models
{
    public class VoucherDTO
    {
        public decimal? Percentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public string Code { get; set; }
        public int DiscountType { get; set; }
    }
}