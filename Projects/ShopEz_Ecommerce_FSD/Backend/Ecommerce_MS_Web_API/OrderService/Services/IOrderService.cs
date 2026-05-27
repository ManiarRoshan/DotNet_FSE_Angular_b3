using OrderService.DTOs;

namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrder(OrderDTO dto);
        Task<List<OrderResponseDto>> GetAllOrders();
        Task<OrderResponseDto?> GetOrderById(int id);
        Task<List<OrderResponseDto>> GetOrdersByUserId(int userId);
        Task<OrderResponseDto?> CancelOrder(int orderId, int userId, bool isAdmin);
    }
}
