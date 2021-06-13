using System.Collections.Generic;

namespace ESE.Store.MVC.Models
{
    public class CustomerCartViewModel
    {
        public decimal TotalValue { get; set; }
        public VoucherViewModel Voucher { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }
        public List<ItemCartViewModel> Items { get; set; } = new List<ItemCartViewModel>();
    }
}
