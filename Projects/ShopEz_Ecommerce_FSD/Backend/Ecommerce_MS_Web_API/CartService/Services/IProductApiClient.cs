namespace CartService.Services
{
    public interface IProductApiClient
    {
        Task<ProductDto?> GetProduct(int productId);
        Task<bool> UpdateStock(int productId, int newStock);
    }
}
