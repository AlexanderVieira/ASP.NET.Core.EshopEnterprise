using ESE.Order.API.Application.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Order.API.Application.Interfaces
{
    public interface IOrderQueries
    {
        Task<OrderDTO> GetLastOrder(Guid clientId);
        Task<IEnumerable<OrderDTO>> GetListByClientId(Guid clientId);
    }
}
