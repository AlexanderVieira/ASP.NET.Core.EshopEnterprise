using ESE.Store.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services.Interfaces
{
    public interface IPorchasesBffService
    {
        Task<CustomerCartViewModel> GetCustomerCart();
        Task<int> GetQuantityCustomerCart();
        Task<ResponseResult> AddItemCart(ItemCartViewModel item);
        Task<ResponseResult> UpdateItemCart(Guid productId, ItemCartViewModel item);
        Task<ResponseResult> RemoveItemCart(Guid productId);
        Task<ResponseResult> ApplyVoucherCustomerCart(string voucher);

        Task<ResponseResult> Checkout(OrderTransactionViewModel orderTransaction);
        Task<OrderViewModel> GetLastOrder();
        Task<IEnumerable<OrderViewModel>> GetListByClientId();
        OrderTransactionViewModel MapToOrder(CustomerCartViewModel customerCart, AddressViewModel address);
    }
}
