using System.Text.Json.Serialization;

namespace ECommerce_API.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        [JsonIgnore] 
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
