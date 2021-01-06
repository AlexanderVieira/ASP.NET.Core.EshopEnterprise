using ESE.Client.API.Application.Commands;
using ESE.Core.DomainObjects;
using FluentValidation;
using System;

namespace ESE.Client.API.Application.Validations
{
    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido.");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do cliente não foi informado.");

            RuleFor(c => c.Cpf)
                .Must(HasValidCpf)
                .WithMessage("O CPF informado não é válido.");

            RuleFor(c => c.Email)
                .Must(HasValidEmail)
                .WithMessage("O e-mail informado não é válido.");
        }

        private bool HasValidEmail(string email)
        {
            return Email.EmailValid(email);
        }

        protected bool HasValidCpf(string cpf)
        {
            return Cpf.CpfValid(cpf);
        }
    }
}
