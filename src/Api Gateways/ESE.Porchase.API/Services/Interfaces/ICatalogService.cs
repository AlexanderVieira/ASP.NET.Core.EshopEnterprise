using ESE.Porchase.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Porchase.API.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ItemProductDTO>> GetAll(IEnumerable<Guid> ids);
        Task<ItemProductDTO> GetById(Guid id);
    }
}
