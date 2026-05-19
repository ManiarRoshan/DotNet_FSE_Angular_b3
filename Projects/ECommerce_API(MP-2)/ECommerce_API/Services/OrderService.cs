using ECommerce_API.Data;
using ECommerce_API.DTOs;
using ECommerce_API.Models;
using ECommerce_API.Repositories;
using ECommerce_API.Services;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IOrderRepository _repo;

    public OrderService(ApplicationDbContext context, IOrderRepository repo)
    {
        _context = context;
        _repo = repo;
    }

    public async Task<string> CreateOrder(OrderDTO dto)
    {
        if (dto == null || dto.Items == null || dto.Items.Count == 0)
            return "Cart is empty";

        decimal total = 0;
        var order = new Order
        {
            UserId = dto.UserId,
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>()
        };

        foreach (var item in dto.Items)
        {
            // Find product - EF starts tracking it here
            var product = await _context.Products.FindAsync(item.ProductId);

            if (product == null)
                return $"Product ID {item.ProductId} not found";

            if (item.Quantity <= 0)
                return "Quantity must be greater than zero";

            if (product.Stock < item.Quantity)
                return $"Not enough stock for product {product.Name}";

            var orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            };

            total += product.Price * item.Quantity;

            // Reduce stock in memory
            product.Stock -= item.Quantity;

            order.OrderItems.Add(orderItem);
        }

        order.TotalAmount = total;

        // Add the new order to the context
        await _context.Orders.AddAsync(order);

        await _context.SaveChangesAsync();

        return "Order Created Successfully";
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _repo.GetAllOrders();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _repo.GetOrderById(id);
    }
}