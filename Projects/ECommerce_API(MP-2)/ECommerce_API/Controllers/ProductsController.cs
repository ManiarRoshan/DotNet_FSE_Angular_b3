using ECommerce_API.DTOs;
using ECommerce_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service) => _service = service;

        [HttpGet] // Public - No Token Needed for viewing
        public async Task<IActionResult> GetAll() => Ok(await _service.GetProducts());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProduct(id);
            return product == null ? NotFound() : Ok(product);
        }

        [Authorize(Roles = "Admin")] // Requires Admin Token
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            await _service.AddProduct(dto);
            return Ok("Product Added Successfully");
        }

        [Authorize(Roles = "Admin")] // Requires Admin Token
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductDTO dto)
        {
            await _service.UpdateProduct(id, dto);
            return Ok("Product Updated Successfully");
        }

        [Authorize(Roles = "Admin")] // Requires Admin Token
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProduct(id);
            return Ok("Product Deleted Successfully");
        }
    }
}
