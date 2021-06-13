using ESE.Order.Domain.Model;
using System;

namespace ESE.Order.API.Application.DTO
{
    public class ItemCartDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }

        public static OrderItem ToOrderItem(ItemCartDTO itemCartDTO)
        {
            return new OrderItem(itemCartDTO.ProductId, itemCartDTO.Name, itemCartDTO.Quantity, itemCartDTO.Value, itemCartDTO.Image);
        }
    }
}