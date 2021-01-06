using ESE.Client.API.Application.Validations;
using ESE.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.Client.API.Application.Commands
{
    public class RegisterCustomerCommand : Command
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }

        public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
        {
            AggregateId = id;
            Id = id;
            Name = name;
            Email = email;
            Cpf = cpf;
        }

        public override bool IsValid()
        {
            ValidationResult = new RegisterCustomerValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
