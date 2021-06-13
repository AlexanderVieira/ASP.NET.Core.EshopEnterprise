using ESE.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace ESE.Order.Domain.Model.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderById(Guid id);
        Task<IEnumerable<Order>> GetListByClientId(Guid clientId);
        void Add(Order order);
        void Update(Order order);
        DbConnection GetConnection();
        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
    }
}
