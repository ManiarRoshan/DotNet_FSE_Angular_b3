using CategoryService.Models;
using CategoryService.Repositories;

namespace CategoryService.Services
{
    public class CategoryMService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryMService(ICategoryRepository repository) => _repository = repository;

        public async Task<IEnumerable<Category>> GetCategory() => await _repository.GetAllAsync();
        public async Task<Category?> GetCategoryById(int id) => await _repository.GetByIdAsync(id);
        public async Task CreateCategory(Category category) => await _repository.AddAsync(category);
        public async Task UpdateCategory(Category category) => await _repository.UpdateAsync(category);
        public async Task<bool> RemoveCategory(int id)
        {
            var exists = await _repository.GetByIdAsync(id);
            if (exists == null) return false;
            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
