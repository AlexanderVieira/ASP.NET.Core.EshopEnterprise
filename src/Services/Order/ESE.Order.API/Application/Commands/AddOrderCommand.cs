using ESE.Core.Messages;
using ESE.Order.API.Application.DTO;
using ESE.Order.API.Validations;
using System;
using System.Collections.Generic;

namespace ESE.Order.API.Application.Commands
{
    public class AddOrderCommand : Command
    {
        //Order
        public Guid ClientId { get; set; }
        public decimal TotalValue { get; set; }
        public List<OrderItemtDTO> OrderItems { get; set; }

        //Voucher
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public decimal Discount { get; set; }

        //Address
        public AddressDTO Address { get; set; }

        //Card
        public string NumberCard { get; set; }        
        public string NameCard { get; set; }        
        public string ExpirationCard { get; set; }        
        public string CvvCard { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
