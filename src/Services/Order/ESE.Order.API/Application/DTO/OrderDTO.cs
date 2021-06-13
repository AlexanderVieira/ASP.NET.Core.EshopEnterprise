using System;
using System.Collections.Generic;

namespace ESE.Order.API.Application.DTO
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }
        public int Code { get; set; }
        // Autorizado = 1,
        // Pago = 2,
        // Recusado = 3,
        // Entregue = 4,
        // Cancelado = 5
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }
        public bool VoucherUsed { get; set; }
        public List<ItemCartDTO> OrderItems { get; set; }
        public AddressDTO Address { get; set; }       

        public static OrderDTO ToOrderDTO(Domain.Model.Order order)
        {
            var orderDTO = new OrderDTO
            {
                OrderId = order.Id,
                Code = order.Code,
                Status = (int)order.OrderStatus,
                Date = order.DateRegister,
                TotalValue = order.TotalValue,
                Discount = order.Discount,
                VoucherUsed = order.VoucherUsed,
                OrderItems = new List<ItemCartDTO>(),
                Address = new AddressDTO()                
            };

            foreach (var item in order.OrderItems)
            {
                orderDTO.OrderItems.Add(new ItemCartDTO 
                {
                    ProductId = item.ProductId,
                    OrderId = item.OrderId,
                    Name = item.ProductName,
                    Quantity = item.Quantity,
                    Value = item.UnitValue,
                    Image = item.ProductImage                    
                });
            }

            orderDTO.Address = new AddressDTO
            {
                Street = order.Address.Street,
                Number = order.Address.Number,
                Complement = order.Address.Complement,
                District = order.Address.District,
                City = order.Address.City,
                State = order.Address.State,
                CodePostal = order.Address.CodePostal
            };

            return orderDTO;
        }
    }
}
