using System.Collections.Generic;

namespace ESE.Store.MVC.Models
{
    public class CustomerCartViewModel
    {
        public decimal TotalValue { get; set; }
        public List<ItemCartViewModel> Items { get; set; } = new List<ItemCartViewModel>();
    }
}
