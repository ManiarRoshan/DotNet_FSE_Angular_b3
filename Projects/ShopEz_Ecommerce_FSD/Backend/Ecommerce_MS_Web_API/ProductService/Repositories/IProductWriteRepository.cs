using ProductService.Models;

namespace ProductService.Repositories
{
    public interface IProductWriteRepository
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(int id);
        Task<Product?> GetById(int id);
    }
}