using ESE.ShoppingCart.API.Models;
using ESE.ShoppingCart.API.Properties;
using FluentValidation;
using System;

namespace ESE.ShoppingCart.API.Validations
{
    public class ItemCartValidation : AbstractValidator<ItemCart>
    {
        public ItemCartValidation()
        {
            RuleFor(i => i.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage(Resources.MSG_ERROR_ID_PRODUCT_INVALID);

            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage(Resources.MSG_ERROR_PRODUCT_NAME_EMPTY);

            RuleFor(i => i.Quantity)
                .GreaterThan(0)
                .WithMessage(item => Resources.MSG_ERROR_QUANTITY_MIN);

            RuleFor(i => i.Quantity)
                .LessThanOrEqualTo(int.Parse(Resources.MAX_QUANTITY_ITEM_CUSTOMER_CART))
                .WithMessage(item => $"A quantidade máxima do {item.Name} é {Resources.MAX_QUANTITY_ITEM_CUSTOMER_CART}");

            RuleFor(i => i.Value)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Name} precisa ser maior que 0.");
        }
    }
}
