using ESE.Catalog.API.Models;
using ESE.Catalog.API.Models.Interfaces;
using ESE.WebAPI.Core.Auth;
using ESE.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Catalog.API.Controllers
{    
    [Authorize]
    public class CatalogController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet("catalog/products")]
        public async Task<IEnumerable<Product>> Index()
        {
            return await _productRepository.GetAll();
        }

        [HttpGet("catalog/products/list/{ids}")]
        public async Task<IEnumerable<Product>> GetProductsById(string ids)
        {
            return await _productRepository.GetProductsById(ids);
        }

        //[ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> ProductDetail(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}
