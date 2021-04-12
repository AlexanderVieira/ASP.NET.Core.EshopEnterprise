using System.Collections.Generic;

namespace ESE.Porchase.API.Models
{
    public class CustomerCartDTO
    {
        public decimal TotalValue { get; set; }
        public VoucherDTO Voucher { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCartDTO> Items { get; set; } = new List<ItemCartDTO>();
    }
}
