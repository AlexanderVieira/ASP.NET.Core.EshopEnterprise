using ESE.Core.Data.Interfaces;
using ESE.Order.Domain.Model;
using ESE.Order.Domain.Model.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.Order.Infra.Data.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _ctx;

        public OrderRepository(OrderContext ctx)
        {
            _ctx = ctx;
        }

        public IUnitOfWork UnitOfWork => _ctx;

        public void Add(Domain.Model.Order order)
        {
            _ctx.Orders.Add(order);
        }

        public void Update(Domain.Model.Order order)
        {
            _ctx.Orders.Update(order);
        }

        public async Task<IEnumerable<Domain.Model.Order>> GetListByClientId(Guid clientId)
        {
            return await _ctx.Orders.Include(p => p.OrderItems).AsNoTracking().Where(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task<Domain.Model.Order> GetOrderById(Guid id)
        {
            return await _ctx.Orders.FindAsync(id);
        }

        public DbConnection GetConnection()
        {
            return _ctx.Database.GetDbConnection();
        }

        public async Task<OrderItem> GetItemById(Guid id)
        {
            return await _ctx.OrderItems.FindAsync(id);
        }

        public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
        {
            return await _ctx.OrderItems.FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);
        }        
        
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
