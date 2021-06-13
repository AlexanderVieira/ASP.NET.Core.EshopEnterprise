using ESE.Catalog.API.Models;
using ESE.Catalog.API.Models.Interfaces;
using ESE.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Product>> GetProductsById(string ids)
        {
            var identifiers = ids.Split(',').Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));
            if (!identifiers.All(nid => nid.Ok)) return new List<Product>();
            var identifiersValue = identifiers.Select(id => id.Value);
            return await _ctx.Products.AsNoTracking().Where(p => identifiersValue.Contains(p.Id) && p.Active).ToListAsync();
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
