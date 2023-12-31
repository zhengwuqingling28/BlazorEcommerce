﻿using BlazorEcommerce.Server.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAdminProducts()
        {
            var response = await _service.GetAdminProducts();
            return Ok(response);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> CreateProduct(Product product)
        {
            var response = await _service.CreateProduct(product);
            return Ok(response);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Product>>> UpdateProduct(Product product)
        {
            var response = await _service.UpdateProduct(product);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpDelete("{productId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> SoftDeleteProduct(int productId)
        {
            var response = await _service.SoftDeleteProduct(productId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts() 
        {
            var response = await _service.GetProductsAsync();
            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
        {
            var response = await _service.GetProductAsync(productId);
            if(response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory(string categoryUrl)
        {
            var response = await _service.GetProductByCategories(categoryUrl);
            if (response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts (string searchText, int page)
        {
            var response = await _service.SearchProducts(searchText, page);
            if(response.Success == false)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpGet("SearchSuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> GetProductSearchSuggestions(string searchText )
        {
            var response = await _service.GetProductSearchSuggestions(searchText);
            return Ok(response);
        }
    }
}
