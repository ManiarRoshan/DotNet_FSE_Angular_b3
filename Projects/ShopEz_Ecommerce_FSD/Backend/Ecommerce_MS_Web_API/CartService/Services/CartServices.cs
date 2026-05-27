using CartService.DTOs;
using CartService.Models;
using CartService.Repositories;

namespace CartService.Services
{
    public class CartServices : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductApiClient _productApi;

        public CartServices(ICartRepository cartRepo, IProductApiClient productApi)
        {
            _cartRepo = cartRepo;
            _productApi = productApi;
        }

        public async Task<CartResponseDto> GetCart(int userId)
        {
            var cartItems = await _cartRepo.GetUserCart(userId);
            var response = new CartResponseDto();
            decimal total = 0;

            foreach (var item in cartItems)
            {
                var product = await _productApi.GetProduct(item.ProductId);
                if (product != null)
                {
                    var dto = new CartItemDto
                    {
                        ProductId = item.ProductId,
                        ProductName = product.Name,
                        Price = product.Price,
                        Quantity = item.Quantity
                    };
                    response.Items.Add(dto);
                    total += dto.Subtotal;
                    response.TotalItems += item.Quantity;
                }
            }
            response.TotalAmount = total;
            return response;
        }

        public async Task AddToCart(int userId, AddToCartDto dto)
        {
            var product = await _productApi.GetProduct(dto.ProductId);
            if (product == null) throw new Exception("Product not found");

            var existing = await _cartRepo.GetCartItem(userId, dto.ProductId);
            int newQty = (existing?.Quantity ?? 0) + dto.Quantity;
            if (newQty > product.Stock) throw new Exception($"Only {product.Stock} in stock");

            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = newQty
            };
            await _cartRepo.AddOrUpdateCartItem(cartItem);
        }

        public async Task UpdateQuantity(int userId, int productId, int quantity)
        {
            var product = await _productApi.GetProduct(productId);
            if (product == null) throw new Exception("Product not found");
            if (quantity > product.Stock) throw new Exception($"Only {product.Stock} in stock");
            if (quantity <= 0) await RemoveFromCart(userId, productId);
            else
            {
                var cartItem = new CartItem { UserId = userId, ProductId = productId, Quantity = quantity };
                await _cartRepo.AddOrUpdateCartItem(cartItem);
            }
        }

        public async Task RemoveFromCart(int userId, int productId) =>
            await _cartRepo.RemoveCartItem(userId, productId);

        public async Task ClearCart(int userId) =>
            await _cartRepo.ClearCart(userId);
    }
}