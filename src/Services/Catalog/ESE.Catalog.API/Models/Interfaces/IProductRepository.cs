using ESE.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Catalog.API.Models.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAll();
        Task<List<Product>> GetProductsById(string ids);
        Task<Product> GetById(Guid id);
        void Add(Product product);
        void Update(Product product);
    }
}
