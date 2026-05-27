using CartService.DTOs;

namespace CartService.Services
{
    public interface ICartService
    {
        Task<CartResponseDto> GetCart(int userId);
        Task AddToCart(int userId, AddToCartDto dto);
        Task RemoveFromCart(int userId, int productId);
        Task UpdateQuantity(int userId, int productId, int quantity);
        Task ClearCart(int userId);
    }
}