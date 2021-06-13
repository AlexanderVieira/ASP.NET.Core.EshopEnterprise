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
        public decimal Discount { get; set; }
        public bool VoucherUsed { get; set; }
        public Voucher Voucher { get; set; }
        public List<ItemCart> Items { get; set; } = new List<ItemCart>();
        public ValidationResult ValidationResult { get; set; }

        public CustomerCart(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
        }

        public CustomerCart()
        {
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            VoucherUsed = true;
            CalculateCartValue();
        }

        internal void CalculateCartValue()
        {
            TotalValue = Items.Sum(i => i.CalculateValue());
            CalculateTotalValueDiscount();
        }

        public void CalculateTotalValueDiscount()
        {
            if (!VoucherUsed) return;
            decimal discount = 0;
            var value = TotalValue;
            if (Voucher.DiscountType == DiscountVoucherType.Percentage)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (value * Voucher.Percentage.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.TotalDiscount.HasValue)
                {
                    discount = Voucher.TotalDiscount.Value;
                    value -= discount;
                }
            }

            TotalValue = value < 0 ? 0 : value;
            Discount = discount;
        }

        internal bool ExistingItemCart(ItemCart item) 
        {
            return Items.Any(i => i.ProductId == item.ProductId);
        }

        internal ItemCart GetByProductId(Guid productId)
        {
            return Items.FirstOrDefault(i => i.ProductId == productId);
        }

        internal void AddItem(ItemCart item)
        {
            item.AssociateCustomerCart(Id);
            if (ExistingItemCart(item))
            {
                var existingItem = GetByProductId(item.ProductId);
                existingItem.AddUnits(item.Quantity);

                item = existingItem;
                Items.Remove(existingItem);
            }

            Items.Add(item);
            CalculateCartValue();
        }

        internal void UpdateItem(ItemCart item)
        {
            item.AssociateCustomerCart(Id);
            var existingItem = GetByProductId(item.ProductId);                      
            Items.Remove(existingItem);
            Items.Add(item);
            CalculateCartValue();
        }

        internal void UpdateUnits(ItemCart item, int units)
        {
            item.UpdateUnits(units);
            UpdateItem(item);
        }

        internal void RemoveItem(ItemCart item)
        {
            Items.Remove(GetByProductId(item.ProductId));
            CalculateCartValue();
        }

        internal bool IsValid()
        {
            var errors = Items.SelectMany(item => new ItemCartValidation().Validate(item).Errors).ToList();
            errors.AddRange(new CustomerCartValidation().Validate(this).Errors);
            ValidationResult = new ValidationResult(errors);
            return ValidationResult.IsValid;
        }

    }
}
