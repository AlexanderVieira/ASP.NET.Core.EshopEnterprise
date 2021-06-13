using ESE.Core.DomainObjects;
using ESE.Core.DomainObjects.Interfaces;
using ESE.Order.Domain.Vouchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESE.Order.Domain.Model
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalValue { get; private set; }
        public DateTime DateRegister { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public Address Address { get; private set; }
        public Voucher Voucher { get; private set; }
        
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(Guid clientId, decimal totalValue, List<OrderItem> orderItems, decimal discount = 0, bool voucherUsed = false, Guid? voucherId = null)
        {
            ClientId = clientId;
            VoucherId = voucherId;
            VoucherUsed = voucherUsed;
            Discount = discount;
            TotalValue = totalValue;
            _orderItems = orderItems;
        }

        protected Order()
        {

        }

        public void AuthorizeOrder()
        {
            OrderStatus = OrderStatus.Autorizado;
        }

        public void AssignVoucher(Voucher voucher)
        {
            VoucherUsed = true;
            VoucherId = voucher.Id;
            Voucher = voucher;
        }

        public void AssignAddress(Address address)
        {
            Address = address;
        }

        public void CalculateOrderAmount()
        {
            TotalValue = OrderItems.Sum(p => p.CalculateValue());
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
    }
}
