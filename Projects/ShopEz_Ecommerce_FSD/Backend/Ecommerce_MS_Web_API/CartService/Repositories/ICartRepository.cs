using CartService.Models;

namespace CartService.Repositories
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetUserCart(int userId);
        Task<CartItem?> GetCartItem(int userId, int productId);
        Task AddOrUpdateCartItem(CartItem item);
        Task RemoveCartItem(int userId, int productId);
        Task ClearCart(int userId);
    }
}