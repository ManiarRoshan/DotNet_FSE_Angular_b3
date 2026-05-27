using System.Text.Json.Serialization;

namespace OrderService.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Pending";
        public string OrderStatus { get; set; } = "Placed";

        [JsonIgnore]
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
