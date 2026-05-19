using ECommerce_API.DTOs;
using ECommerce_API.Models;
using ECommerce_API.Repositories;

namespace ECommerce_API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _repo.GetAll();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _repo.GetById(id);
        }

        public async Task AddProduct(ProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Stock = dto.Stock
            };

            await _repo.Add(product);
        }

        public async Task UpdateProduct(int id, ProductDTO dto)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                return;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.Stock = dto.Stock;

            await _repo.Update(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await _repo.GetById(id);

            if (product == null)
                return;

            await _repo.Delete(product);
        }
    }
}
