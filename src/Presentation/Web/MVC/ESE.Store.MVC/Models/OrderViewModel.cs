using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Models
{
    public class OrderViewModel
    {
        #region Order

        public int Code { get; set; }
        // Autorizado = 1,
        // Pago = 2,
        // Recusado = 3,
        // Entregue = 4,
        // Cancelado = 5
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public List<ItemCartViewModel> OrderItems { get; set; } = new List<ItemCartViewModel>();

        #endregion

        #region Address

        public AddressViewModel Address { get; set; }

        #endregion Address
        
    }
}
