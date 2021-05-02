using ESE.Core.Data.Interfaces;
using ESE.Order.Domain.Vouchers;
using ESE.Order.Domain.Vouchers.Inferfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ESE.Order.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrderContext _ctx;

        public VoucherRepository(OrderContext ctx)
        {
            _ctx = ctx;
        }

        public IUnitOfWork UnitOfWork => _ctx;
       
        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _ctx.Vouchers.FirstOrDefaultAsync(v => v.Code == code);
        }

        public void Update(Voucher voucher)
        {
            _ctx.Vouchers.Update(voucher);
        }
        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
