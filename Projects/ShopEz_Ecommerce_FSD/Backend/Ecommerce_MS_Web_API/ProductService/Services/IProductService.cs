using ProductService.Models;
using ProductService.DTOs;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<IEnumerable<Product>> GetProductsForAdmin();
        Task<Product?> GetProduct(int id);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsWithFilters(ProductQueryParameters parameters);
        Task AddProduct(ProductDTO dto);
        Task UpdateProduct(int id, ProductDTO dto);
        Task DeleteProduct(int id);
        Task SoftDeleteProduct(int id);
        Task UpdateStock(int id, int newStock);
    }
}