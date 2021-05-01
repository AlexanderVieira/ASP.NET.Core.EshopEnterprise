using ESE.Core.DomainObjects;
using System;

namespace ESE.Order.Domain.Model
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitValue { get; private set; }
        public string ProductImage { get; private set; }
        public int Quantity { get; private set; }
        public Order Order { get; set; }

        public OrderItem(Guid orderId, Guid productId, string productName, 
                         decimal unitValue, int quantity, string productImage = null)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitValue = unitValue;
            ProductImage = productImage;
            Quantity = quantity;
        }

        protected OrderItem()
        {

        }

        internal decimal CalculateValue()
        {
            return Quantity * UnitValue;
        }
    }
}