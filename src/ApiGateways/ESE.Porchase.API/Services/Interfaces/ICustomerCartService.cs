using ESE.Core.Comunication;
using ESE.Porchase.API.Models;
using System;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services.Interfaces
{
    public interface ICustomerCartService
    {
        Task<CustomerCartDTO> GetCustomerCart();
        Task<ResponseResult> AddItemCart(ItemCartDTO item);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartDTO item);
        Task<ResponseResult> RemoveItemCart(Guid productId);
        Task<ResponseResult> ApplyVoucherCart(VoucherDTO voucher);

    }
}
