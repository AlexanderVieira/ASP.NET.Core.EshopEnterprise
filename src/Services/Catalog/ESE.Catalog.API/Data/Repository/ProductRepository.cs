using ESE.Catalog.API.Models;
using ESE.Catalog.API.Models.Interfaces;
using ESE.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Catalog.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public ProductRepository(CatalogContext ctx)
        {
            _ctx = ctx;
        }            

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _ctx.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _ctx.Products.FindAsync(id);
        }

        public void Add(Product product)
        {
            _ctx.Products.Add(product);
        }

        public void Update(Product product)
        {
            _ctx.Products.Update(product);
        }

        public void Dispose()
        {
            _ctx?.Dispose();
        }
    }
}
