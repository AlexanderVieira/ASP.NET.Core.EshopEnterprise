using Dapper;
using ESE.Order.API.Application.DTO;
using ESE.Order.API.Application.Interfaces;
using ESE.Order.Domain.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESE.Order.API.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderDTO> GetLastOrder(Guid clientId)
        {
            const string sql = @"SELECT P.ID AS 'ProductId', P.CODE, P.VOUCHERUSED, P.DISCOUNT, P.TOTALVALUE, P.ORDERSTATUS,
                                P.LOGRADOURO, P.NUMERO, P.BAIRRO, P.CEP, P.COMPLEMENTO, P.CIDADE, P.ESTADO,
                                PIT.ID AS 'ProductItemId', PIT.PRODUCTNAME, PIT.QUATITY, PIT.PRODUCTIMAGE, PIT.UNITVALUE
                                FROM PEDIDOS P
                                INNER JOIN ORDERITEMS PIT ON P.ID = PIT.ORDERID
                                WHERE P.CLIENTID = @clientId
                                AND P.DATEREGISTER between DATEADD(minute, -3, GETDATE()) AND DATEADD(minute, 0, GETDATE())
                                AND P.ORDERSTATUS = 1
                                ORDER BY P.DATEREGISTER DESC";

            var order = await _orderRepository.GetConnection()
                .QueryAsync<dynamic>(sql, new { clientId });

            return MappingOrder(order);
        }


        public async Task<IEnumerable<OrderDTO>> GetListByClientId(Guid clientId)
        {
            var orders = await _orderRepository.GetListByClientId(clientId);
            return orders.Select(OrderDTO.ToOrderDTO);

        }

        private OrderDTO MappingOrder(dynamic result)
        {
            var order = new OrderDTO
            {
                Code = result[0].CODE,
                Status = result[0].ORDERSTATUS,
                TotalValue = result[0].TOTALVALUE,
                Discount =  result[0].DISCOUNT,
                VoucherUsed = result[0].VOUCHERUSED,

                OrderItems = new List<OrderItemtDTO>(),
                Address = new AddressDTO 
                { 
                    Street = result[0].LOGRADOURO,
                    District = result[0].BAIRRO,
                    City = result[0].CIDADE,
                    State = result[0].ESTADO,
                    CodePostal = result[0].CEP,
                    Number = result[0].NUMERO,
                    Complement = result[0].COMPLEMENTO
                }
            };

            foreach (var item in result)
            {
                var orderItem = new OrderItemtDTO
                {
                    Name = item.PRODUCTNAME,
                    Value = item.UNITVALUE,
                    Quantity = item.QUANTITY,
                    Image = item.PRODUCTIMAGE
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }
        
    }
}
