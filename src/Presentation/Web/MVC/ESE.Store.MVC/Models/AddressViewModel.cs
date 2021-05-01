using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESE.Store.MVC.Models
{
    public class AddressViewModel
    {
        [Required]
        public string Street { get; set; }
        [Required]
        [DisplayName("Número")]
        public string Number { get; set; }
        public string Complement { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        [DisplayName("CEP")]
        public string CodePostal { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }

        public override string ToString()
        {
            return $"{Street}, {Number} {Complement} - {District} - {City} - {State}";
        }
               
    }
}
