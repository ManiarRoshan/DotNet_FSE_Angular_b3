using ProductService.Models;
using ProductService.DTOs;

namespace ProductService.Repositories
{
    public interface IProductReadRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetAllProductsForAdmin();
        Task<Product?> GetProductById(int id);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsWithFilters(ProductQueryParameters parameters);
    }
}