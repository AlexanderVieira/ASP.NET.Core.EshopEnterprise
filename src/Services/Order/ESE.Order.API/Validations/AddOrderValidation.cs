using ESE.Order.API.Application.Commands;
using FluentValidation;
using System;

namespace ESE.Order.API.Validations
{
    public class AddOrderValidation : AbstractValidator<AddOrderCommand>
    {
        public AddOrderValidation()
        {
            RuleFor(c => c.ClientId)
                    .NotEqual(Guid.Empty)
                    .WithMessage("Id do cliente inválido.");
        }
    }
}
