using ESE.Core.Data.Interfaces;
using System.Threading.Tasks;

namespace ESE.Order.Domain.Vouchers.Inferfaces
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucherByCode(string code);
        void Update(Voucher voucher);
    }
}
