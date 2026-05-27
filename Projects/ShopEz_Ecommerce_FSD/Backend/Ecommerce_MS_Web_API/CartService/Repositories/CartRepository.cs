using Microsoft.EntityFrameworkCore;
using CartService.Data;
using CartService.Models;

namespace CartService.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;
        public CartRepository(CartDbContext context) => _context = context;

        public async Task<List<CartItem>> GetUserCart(int userId) =>
            await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();

        public async Task<CartItem?> GetCartItem(int userId, int productId) =>
            await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        public async Task AddOrUpdateCartItem(CartItem item)
        {
            var existing = await GetCartItem(item.UserId, item.ProductId);
            if (existing != null)
            {
                existing.Quantity = item.Quantity;
                _context.CartItems.Update(existing);
            }
            else
                await _context.CartItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartItem(int userId, int productId)
        {
            var item = await GetCartItem(userId, productId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCart(int userId)
        {
            var items = await GetUserCart(userId);
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}