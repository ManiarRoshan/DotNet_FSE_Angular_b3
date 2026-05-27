using Dapper;
using ProductService.Data;
using ProductService.Models;
using ProductService.DTOs;
using System.Text;

namespace ProductService.Repositories
{
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly DapperContext _context;

        public ProductReadRepository(DapperContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var query = "SELECT * FROM Products ORDER BY ProductId DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Product>(query);
        }

        public async Task<IEnumerable<Product>> GetAllProductsForAdmin()
        {
            var query = "SELECT * FROM Products ORDER BY ProductId DESC";
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Product>(query);
        }

        public async Task<Product?> GetProductById(int id)
        {
            var query = "SELECT * FROM Products WHERE ProductId = @Id";
            using var connection = _context.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Product>(query, new { Id = id });
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetProductsWithFilters(ProductQueryParameters parameters)
        {
            var queryBuilder = new StringBuilder("SELECT * FROM Products WHERE 1=1");
            var countBuilder = new StringBuilder("SELECT COUNT(*) FROM Products WHERE 1=1");
            var param = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                var search = $"%{parameters.SearchTerm}%";
                queryBuilder.Append(" AND (Name LIKE @Search OR Description LIKE @Search)");
                countBuilder.Append(" AND (Name LIKE @Search OR Description LIKE @Search)");
                param.Add("@Search", search);
            }
            if (!string.IsNullOrWhiteSpace(parameters.Category))
            {
                queryBuilder.Append(" AND Category = @Category");
                countBuilder.Append(" AND Category = @Category");
                param.Add("@Category", parameters.Category);
            }
            if (parameters.MinPrice.HasValue)
            {
                queryBuilder.Append(" AND Price >= @MinPrice");
                countBuilder.Append(" AND Price >= @MinPrice");
                param.Add("@MinPrice", parameters.MinPrice.Value);
            }
            if (parameters.MaxPrice.HasValue)
            {
                queryBuilder.Append(" AND Price <= @MaxPrice");
                countBuilder.Append(" AND Price <= @MaxPrice");
                param.Add("@MaxPrice", parameters.MaxPrice.Value);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                var sortField = parameters.SortBy.ToLower() switch
                {
                    "price" => "Price",
                    "name" => "Name",
                    "stock" => "Stock",
                    _ => "ProductId"
                };
                var sortOrder = parameters.SortDescending ? "DESC" : "ASC";
                queryBuilder.Append($" ORDER BY {sortField} {sortOrder}");
            }
            else
            {
                queryBuilder.Append(" ORDER BY ProductId DESC");
            }

            // Pagination
            var offset = (parameters.PageNumber - 1) * parameters.PageSize;
            queryBuilder.Append(" OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            param.Add("@Offset", offset);
            param.Add("@PageSize", parameters.PageSize);

            using var connection = _context.CreateConnection();
            var totalCount = await connection.ExecuteScalarAsync<int>(countBuilder.ToString(), param);
            var products = await connection.QueryAsync<Product>(queryBuilder.ToString(), param);

            return (products, totalCount);
        }
    }
}
