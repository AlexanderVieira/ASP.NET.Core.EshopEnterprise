using System;
using System.Collections.Generic;

namespace ESE.Porchase.API.Models
{
    public class OrderDTO
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
        public List<ItemCartDTO> OrderItems { get; set; }

        #endregion

        #region Address

        public AddressDTO Address { get; set; }

        #endregion Address

        #region Card

        public string NumberCard { get; set; }
        public string NameCard { get; set; }
        public string ExpirationCard { get; set; }
        public string CvvCard { get; set; }

        #endregion

    }
}
