using ECommerce_API.Models;

namespace ECommerce_API.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrder(Order order);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
    }
}