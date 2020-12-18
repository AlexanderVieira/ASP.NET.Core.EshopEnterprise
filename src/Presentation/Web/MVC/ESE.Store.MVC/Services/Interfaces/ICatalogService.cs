using ESE.Store.MVC.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Store.MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
    }
}
