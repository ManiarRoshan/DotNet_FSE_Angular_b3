using ECommerce_API.DTOs;
using ECommerce_API.Models;

namespace ECommerce_API.Services
{
    public interface IOrderService
    {
        Task<string> CreateOrder(OrderDTO dto);
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
    }
}
