using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;
using ProductService.Services;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetProducts();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] ProductQueryParameters parameters)
        {
            var (products, totalCount) = await _service.GetProductsWithFilters(parameters);
            return Ok(new { totalCount, parameters.PageNumber, parameters.PageSize, products });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProduct(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            await _service.AddProduct(dto);
            return Ok(new { message = "Product added" });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDTO dto)
        {
            await _service.UpdateProduct(id, dto);
            return Ok(new { message = "Product updated" });
        }
        

     
        [HttpPut("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] int newStock)
        {
            await _service.UpdateStock(id, newStock);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            var products = await _service.GetProductsForAdmin();
            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.SoftDeleteProduct(id);
            return Ok(new { message = "Product removed from store (soft delete)." });
        }
    }
}