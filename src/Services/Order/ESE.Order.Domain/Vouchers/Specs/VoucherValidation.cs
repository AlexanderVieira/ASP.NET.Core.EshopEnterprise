using NetDevPack.Specification;

namespace ESE.Order.Domain.Vouchers.Specs
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dateSpec = new VoucherDateSpecification();
            var qtdeSpec = new VoucherQuantitySpecification();
            var activeSpec = new VoucherActiveSpecification();

            Add("dateSpec", new Rule<Voucher>(dateSpec, "Este voucher está expirado."));
            Add("qtdeSpec", new Rule<Voucher>(qtdeSpec, "Este voucher já foi utilizado."));
            Add("activeSpec", new Rule<Voucher>(activeSpec, "Este voucher não está mais ativo."));
        }
    }
}
