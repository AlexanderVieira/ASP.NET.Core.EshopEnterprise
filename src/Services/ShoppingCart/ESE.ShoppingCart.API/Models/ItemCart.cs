using ESE.ShoppingCart.API.Validations;
using System;
using System.Text.Json.Serialization;

namespace ESE.ShoppingCart.API.Models
{
    public class ItemCart
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public Guid CustomerCartId { get; set; }

        [JsonIgnore]
        public CustomerCart CustomerCart { get; set; }

        public ItemCart()
        {
            Id = Guid.NewGuid();
        }
        
        internal void AssociateCustomerCart(Guid customerCartId)
        {
            CustomerCartId = customerCartId;
        }

        internal decimal CalculateValue()
        {
            return Quantity * Value;
        }

        internal void AddUnits(int units)
        {
            Quantity += units;
        }

        internal void UpdateUnits(int units)
        {
            Quantity = units;
        }

        internal bool IsValid()
        {
            return new ItemCartValidation().Validate(this).IsValid;
        }
    }
}
