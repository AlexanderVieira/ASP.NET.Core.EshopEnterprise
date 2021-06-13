using ESE.Core.DomainObjects;
using ESE.Core.DomainObjects.Interfaces;
using ESE.Order.Domain.Vouchers.Specs;
using System;

namespace ESE.Order.Domain.Vouchers
{
    public class Voucher : Entity, IAggregateRoot
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? TotalDiscount { get; private set; }
        public int Quantity { get; private set; }
        public DiscountVoucherType DiscountType { get; private set; }
        public DateTime DateCreate { get; private set; }
        public DateTime? DateOfUse { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public bool Active { get; private set; }
        public bool Used { get; private set; }

        public bool IsValidForUse()
        {
            return new VoucherActiveSpecification()
                .And(new VoucherDateSpecification())
                .And(new VoucherQuantitySpecification())
                .IsSatisfiedBy(this);
        }

        public void MarkAsUsed()
        {
            Active = false;
            Used = true;
            Quantity = 0;
            DateOfUse = DateTime.Now;
        }

        public void DebitQuantity()
        {
            Quantity -= 1;
            if (Quantity >= 1) return;
            MarkAsUsed();
        }
    }
}