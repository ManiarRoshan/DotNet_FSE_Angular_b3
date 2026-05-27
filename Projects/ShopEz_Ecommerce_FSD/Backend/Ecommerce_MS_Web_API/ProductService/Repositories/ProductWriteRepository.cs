using Dapper;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repositories
{
    public class ProductWriteRepository : IProductWriteRepository
    {
        private readonly DapperContext _context;

        public ProductWriteRepository(DapperContext context) => _context = context;

        public async Task Add(Product product)
        {
            var query = @"INSERT INTO Products (Name, Description, Price, ImageUrl, Stock, Category) 
                          VALUES (@Name, @Description, @Price, @ImageUrl, @Stock, @Category);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(query, product);
            product.ProductId = id;
        }

        public async Task Update(Product product)
        {
            var query = @"UPDATE Products SET 
                            Name = @Name, Description = @Description, Price = @Price, 
                            ImageUrl = @ImageUrl, Stock = @Stock, Category = @Category 
                          WHERE ProductId = @ProductId";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, product);
        }

        public async Task Delete(int id)
        {
            var query = @"UPDATE Products SET IsDeleted = 1, Stock = 0 WHERE ProductId = @Id";
            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<Product?> GetById(int id)
        {
            var query = "SELECT * FROM Products WHERE ProductId = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }
    }
}