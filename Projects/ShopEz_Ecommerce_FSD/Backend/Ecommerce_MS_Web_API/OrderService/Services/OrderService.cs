using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductApiClient _productApi;

        public OrderService(IOrderRepository orderRepo, IProductApiClient productApi)
        {
            _orderRepo = orderRepo;
            _productApi = productApi;
        }

        public async Task<OrderResponseDto> CreateOrder(OrderDTO dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
                throw new Exception("Cart is empty");
            if (string.IsNullOrWhiteSpace(dto.ShippingAddress))
                throw new Exception("Shipping address required");
            if (string.IsNullOrWhiteSpace(dto.PaymentMethod))
                throw new Exception("Payment method required");

            decimal total = 0;
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                ShippingAddress = dto.ShippingAddress,
                PaymentMethod = dto.PaymentMethod,
                PaymentStatus = "Completed",
                OrderStatus = "Placed",
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                var product = await _productApi.GetProduct(item.ProductId);
                if (product == null)
                    throw new Exception($"Product ID {item.ProductId} not found");
                if (product.IsDeleted)
                    throw new Exception($"Product {product.Name} is no longer available");
                if (item.Quantity <= 0)
                    throw new Exception("Quantity must be greater than zero");
                if (product.Stock < item.Quantity)
                    throw new Exception($"Not enough stock for product {product.Name}");

                var orderItem = new OrderItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.Name,
                    ProductImageUrl = product.ImageUrl ?? string.Empty,
                    Price = product.Price,
                    Quantity = item.Quantity
                };
                total += product.Price * item.Quantity;
                order.OrderItems.Add(orderItem);

                var newStock = product.Stock - item.Quantity;
                var updateSuccess = await _productApi.UpdateStock(item.ProductId, newStock);
                if (!updateSuccess)
                    throw new Exception($"Failed to update stock for product {product.Name}");
            }

            order.TotalAmount = total;
            var createdOrder = await _orderRepo.CreateOrder(order);
            return MapToResponse(createdOrder);
        }

        public async Task<List<OrderResponseDto>> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllOrders();
            return orders.Select(MapToResponse).ToList();
        }

        public async Task<OrderResponseDto?> GetOrderById(int id)
        {
            var order = await _orderRepo.GetOrderById(id);
            return order == null ? null : MapToResponse(order);
        }

        public async Task<List<OrderResponseDto>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepo.GetOrdersByUserId(userId);
            return orders.Select(MapToResponse).ToList();
        }

        public async Task<OrderResponseDto?> CancelOrder(int orderId, int userId, bool isAdmin)
        {
            var order = await _orderRepo.GetOrderById(orderId);
            if (order == null) return null;
            if (!isAdmin && order.UserId != userId)
                throw new UnauthorizedAccessException("You can only cancel your own orders.");
            if (order.OrderStatus == "Cancelled")
                throw new Exception("Order is already cancelled.");

            var updated = await _orderRepo.UpdateOrderStatus(orderId, "Cancelled");
            if (!updated) return null;

            foreach (var item in order.OrderItems)
            {
                var product = await _productApi.GetProduct(item.ProductId);
                if (product != null)
                    await _productApi.UpdateStock(item.ProductId, product.Stock + item.Quantity);
            }

            var refreshed = await _orderRepo.GetOrderById(orderId);
            return refreshed == null ? null : MapToResponse(refreshed);
        }

        private OrderResponseDto MapToResponse(Order order)
        {
            var orderDate = order.OrderDate.Kind == DateTimeKind.Unspecified
                ? DateTime.SpecifyKind(order.OrderDate, DateTimeKind.Utc)
                : order.OrderDate.ToUniversalTime();

            return new OrderResponseDto
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = orderDate,
                TotalAmount = order.TotalAmount,
                ShippingAddress = order.ShippingAddress,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                OrderStatus = order.OrderStatus ?? "Placed",
                OrderItems = order.OrderItems.Select(item => new OrderItemResponseDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductImageUrl = item.ProductImageUrl,
                    Price = item.Price,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
    }
}