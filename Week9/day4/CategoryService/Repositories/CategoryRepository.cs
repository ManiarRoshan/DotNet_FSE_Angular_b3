using CategoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryService.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext _context;
        public CategoryRepository(CategoryDbContext context) => _context = context;

        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(int id) =>
            await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
