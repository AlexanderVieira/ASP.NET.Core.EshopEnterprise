using ESE.Client.API.Application.Events;
using ESE.Client.API.Models;
using ESE.Client.API.Models.Interfaces;
using ESE.Core.Messages;
using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ESE.Client.API.Application.Commands
{
    public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand, ValidationResult>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ValidationResult> Handle(RegisterCustomerCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid()) return message.ValidationResult;

            var customer = new Customer(message.Id, message.Name, message.Email, message.Cpf);
            var existingCustomer = await _customerRepository.GetByCpf(customer.Cpf.Number);

            if (existingCustomer != null)
            {
                AddError("Este CPF já está em uso.");
                return ValidationResult;
            }

            _customerRepository.AddCustomer(customer);

            customer.AddEvent(new RegisteredCustomerEvent(message.Id, message.Name, message.Email, message.Cpf));

            return await PersistData(_customerRepository.UnitOfWork);
        }
    }
}
