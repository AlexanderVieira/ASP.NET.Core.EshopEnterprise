using ESE.Core.Data.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Client.API.Models.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        void AddCustomer(Customer customer);
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByCpf(string cpf);
    }
}
