using ESE.Catalog.API.Models;
using ESE.Catalog.API.Models.Interfaces;
using ESE.WebAPI.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESE.Catalog.API.Controllers
{
    [ApiController]
    [Authorize]
    public class CatalogController : Controller
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

        [ClaimsAuthorize("Catalogo", "Ler")]
        [HttpGet("catalog/products/{id}")]
        public async Task<Product> Details(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}
