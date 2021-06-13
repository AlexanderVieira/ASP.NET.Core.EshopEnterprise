using ESE.Order.API.Application.DTO;
using ESE.Order.API.Application.Interfaces;
using ESE.Order.Domain.Vouchers.Inferfaces;
using System.Threading.Tasks;

namespace ESE.Order.API.Application.Queries
{
    public class VoucherQueries : IVoucherQueries
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherQueries(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public async Task<VoucherDTO> GetVoucherByCode(string code)
        {
            var voucher = await _voucherRepository.GetVoucherByCode(code);
            if (voucher == null) return null;
            if (!voucher.IsValidForUse()) return null;
            return new VoucherDTO
            {
                Code = voucher.Code,
                DiscountType = (int)voucher.DiscountType,
                Percentage = voucher.Percentage,
                TotalDiscount = voucher.TotalDiscount
            };
        }
    }
}
