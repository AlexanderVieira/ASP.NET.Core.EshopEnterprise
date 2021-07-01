using ESE.Order.Domain.Model;
using System;

namespace ESE.Order.API.Application.DTO
{
    public class OrderItemtDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }

        public static OrderItem ToOrderItem(OrderItemtDTO orderItemDTO)
        {
            return new OrderItem(orderItemDTO.ProductId, orderItemDTO.Name, orderItemDTO.Quantity, orderItemDTO.Value, orderItemDTO.Image);
        }
    }
}