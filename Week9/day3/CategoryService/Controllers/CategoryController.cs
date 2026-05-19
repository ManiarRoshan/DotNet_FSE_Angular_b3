using CategoryService.Models;
using CategoryService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.GetCategory());

        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {
            await _service.CreateCategory(category);
            return Ok(new { category, status = "Category created!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category category)
        {
            if (id != category.CategoryId) return BadRequest("ID mismatch");
            var existing = await _service.GetCategoryById(id);
            if (existing == null) return NotFound();

            await _service.UpdateCategory(category);
            return Ok(new { category, status = "Updated successfully!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _service.RemoveCategory(id);
            return success ? Ok("Deleted") : NotFound();
        }
    }
}
