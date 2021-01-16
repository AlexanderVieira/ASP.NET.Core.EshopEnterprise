using ESE.ShoppingCart.API.Models;
using ESE.ShoppingCart.API.Properties;
using FluentValidation;
using System;

namespace ESE.ShoppingCart.API.Validations
{
    public class CustomerCartValidation : AbstractValidator<CustomerCart>
    {
        public CustomerCartValidation()
        {
            RuleFor(c => c.CustomerId)
                    .NotEqual(Guid.Empty)
                    .WithMessage(Resources.MSG_ERROR_CUSTOMER_UNKNOWN);

            RuleFor(c => c.Itens.Count)
                .GreaterThan(0)
                .WithMessage(Resources.MSG_ERROR_CART_EMPTY);

            RuleFor(c => c.TotalValue)
                .GreaterThan(0)
                .WithMessage(Resources.MSG_ERROR_TOTAL_VALUE);
        }
    }
}
