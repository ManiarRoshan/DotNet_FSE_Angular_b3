using ECommerce_API.DTOs;
using ECommerce_API.Models;

namespace ECommerce_API.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task AddProduct(ProductDTO dto);
        Task UpdateProduct(int id, ProductDTO dto);
        Task DeleteProduct(int id);
    }
}
