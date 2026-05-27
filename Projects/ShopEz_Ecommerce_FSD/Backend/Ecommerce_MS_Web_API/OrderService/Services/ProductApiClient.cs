using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OrderService.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ProductDto?> GetProduct(int productId)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<ProductDto>($"/api/products/{productId}");
        }

        public async Task<bool> UpdateStock(int productId, int newStock)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"/api/products/{productId}/stock", newStock);
            return response.IsSuccessStatusCode;
        }

        private void AddAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token);
            }
        }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}