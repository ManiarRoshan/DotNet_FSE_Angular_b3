using CategoryService.Models;

namespace CategoryService.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategory();
        Task<Category> GetCategoryById(int id);
        Task CreateCategory(Category category);
        Task UpdateCategory(Category category);
        Task<bool> RemoveCategory(int id);
    }
}
