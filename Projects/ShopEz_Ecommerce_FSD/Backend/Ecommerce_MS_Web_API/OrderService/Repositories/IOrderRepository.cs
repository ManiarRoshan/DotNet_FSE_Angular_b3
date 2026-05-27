using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int id);
        Task<List<Order>> GetOrdersByUserId(int userId);
        Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
