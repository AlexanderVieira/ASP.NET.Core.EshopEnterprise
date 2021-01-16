using ESE.ShoppingCart.API.Validations;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESE.ShoppingCart.API.Models
{
    public class CustomerCart
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public List<ItemCart> Itens { get; set; } = new List<ItemCart>();
        public ValidationResult ValidationResult { get; set; }

        public CustomerCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public CustomerCart()
        {
        }

        internal void CalculateCartValue()
        {
            TotalValue = Itens.Sum(i => i.CalculateValue());
        }

        internal bool ExistingItemCart(ItemCart item) 
        {
            return Itens.Any(i => i.ProductId == item.ProductId);
        }

        internal ItemCart GetByProductId(Guid productId)
        {
            return Itens.FirstOrDefault(i => i.ProductId == productId);
        }

        internal void AddItem(ItemCart item)
        {
            item.AssociateCustomerCart(Id);
            if (ExistingItemCart(item))
            {
                var existingItem = GetByProductId(item.ProductId);
                existingItem.AddUnits(item.Quantity);

                item = existingItem;
                Itens.Remove(existingItem);
            }

            Itens.Add(item);
            CalculateCartValue();
        }

        internal void UpdateItem(ItemCart item)
        {
            item.AssociateCustomerCart(Id);
            var existingItem = GetByProductId(item.ProductId);                      
            Itens.Remove(existingItem);
            Itens.Add(item);
            CalculateCartValue();
        }

        internal void UpdateUnits(ItemCart item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        internal void RemoveItem(ItemCart item)
        {
            Itens.Remove(GetByProductId(item.ProductId));
            CalculateCartValue();
        }

        internal bool IsValid()
        {
            var errors = Itens.SelectMany(item => new ItemCartValidation().Validate(item).Errors).ToList();
            errors.AddRange(new CustomerCartValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);
            return ValidationResult.IsValid;
        }

    }
}
