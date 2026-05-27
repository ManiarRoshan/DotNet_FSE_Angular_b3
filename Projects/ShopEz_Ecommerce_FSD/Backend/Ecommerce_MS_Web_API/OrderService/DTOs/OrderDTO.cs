using System.Collections.Generic;

namespace OrderService.DTOs
{
    public class OrderDTO
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public List<OrderItemDTO> Items { get; set; } = new();
    }

    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}