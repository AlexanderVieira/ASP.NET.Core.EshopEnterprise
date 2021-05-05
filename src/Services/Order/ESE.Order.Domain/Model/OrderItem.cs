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

        public OrderItem(Guid productId, string productName, int quantity, decimal unitValue, string productImage = null)
        {            
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
            ProductImage = productImage;            
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