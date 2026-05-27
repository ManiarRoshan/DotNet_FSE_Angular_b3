namespace OrderService.DTOs
{
    public class ProductFromProductService
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool IsDeleted { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}