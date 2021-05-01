using ESE.Core.Validation;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESE.Store.MVC.Models
{
    public class OrderTransactionViewModel
    {
        #region Order
        
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public List<ItemCartViewModel> OrderItems { get; set; } = new List<ItemCartViewModel>();

        #endregion

        #region Address

        public AddressViewModel Address { get; set; }

        #endregion Address

        #region Card

        [Required(ErrorMessage = "Informe o número do cartão")]
        [DisplayName("Número do Cartão")]
        public string NumberCard { get; set; }

        [Required(ErrorMessage = "Informe o nome do portador do cartão")]
        [DisplayName("Nome do Portador")]
        public string NameCard { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "O vencimento deve estar no padrão MM/AA")]
        [ExpirationCard(ErrorMessage = "Cartão Expirado")]
        [Required(ErrorMessage = "Informe o vencimento")]
        [DisplayName("Data de Vencimento MM/AA")]
        public string ExpirationCard { get; set; }

        [Required(ErrorMessage = "Informe o código de segurança")]
        [DisplayName("Código de Segurança")]
        public string CvvCard { get; set; }

        #endregion
    }
}
