using ProductService.Models;
using ProductService.DTOs;
using ProductService.Repositories;

namespace ProductService.Services
{
    public class ProductServices : IProductService
    {
        private readonly IProductReadRepository _readRepo;
        private readonly IProductWriteRepository _writeRepo;

        public ProductServices(IProductReadRepository readRepo, IProductWriteRepository writeRepo)
        {
            _readRepo = readRepo;
            _writeRepo = writeRepo;
        }

        public async Task<IEnumerable<Product>> GetProducts() => await _readRepo.GetAllProducts();
        public async Task<IEnumerable<Product>> GetProductsForAdmin() => await _readRepo.GetAllProductsForAdmin();
        public async Task<Product?> GetProduct(int id) => await _readRepo.GetProductById(id);
        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsWithFilters(ProductQueryParameters parameters) =>
            await _readRepo.GetProductsWithFilters(parameters);

        public async Task AddProduct(ProductDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl,
                Stock = dto.Stock,
                Category = dto.Category
            };
            await _writeRepo.Add(product);
        }

        public async Task UpdateProduct(int id, ProductDTO dto)
        {
            var product = await _writeRepo.GetById(id);
            if (product == null) throw new Exception("Product not found");

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            product.Stock = dto.Stock;
            product.Category = dto.Category;
            await _writeRepo.Update(product);
        }

        public async Task UpdateStock(int id, int newStock)
        {
            var product = await _writeRepo.GetById(id);
            if (product == null) throw new Exception("Product not found");
            product.Stock = newStock;
            await _writeRepo.Update(product);
        }

        public async Task DeleteProduct(int id) => await _writeRepo.Delete(id);
        public async Task SoftDeleteProduct(int id) => await _writeRepo.Delete(id);
    }
}