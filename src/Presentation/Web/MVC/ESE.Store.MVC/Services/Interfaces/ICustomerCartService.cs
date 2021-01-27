using ESE.Store.MVC.Models;
using System;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services.Interfaces
{
    public interface ICustomerCartService
    {
        Task<CustomerCartViewModel> GetCustomerCart();
        Task<ResponseResult> AddItemCart(ItemCartViewModel item);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartViewModel item);
        Task<ResponseResult> RemoveItemCart(Guid productId);
    }
}
