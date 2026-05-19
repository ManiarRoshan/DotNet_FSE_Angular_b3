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

    public async Task<Order> CreateOrder(OrderDTO dto)
    {
        if (dto == null || dto.Items == null || dto.Items.Count == 0)
            throw new Exception("Cart is empty");

        decimal total = 0;
        var order = new Order
        {
            UserId = dto.UserId,
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>()
        };

        foreach (var item in dto.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null)
                throw new Exception($"Product ID {item.ProductId} not found");

            if (item.Quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            if (product.Stock < item.Quantity)
                throw new Exception($"Not enough stock for product {product.Name}");

            var orderItem = new OrderItem
            {
                ProductId = product.ProductId,
                Quantity = item.Quantity,
                Price = product.Price
            };

            total += product.Price * item.Quantity;
            product.Stock -= item.Quantity;
            order.OrderItems.Add(orderItem);
        }

        order.TotalAmount = total;
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();

        // Load navigation properties before returning
        await _context.Entry(order)
            .Collection(o => o.OrderItems)
            .LoadAsync();
        foreach (var oi in order.OrderItems)
        {
            await _context.Entry(oi)
                .Reference(oi => oi.Product)
                .LoadAsync();
        }

        return order;
    }

    public async Task<List<Order>> GetAllOrders()
    {
        return await _repo.GetAllOrders();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await _repo.GetOrderById(id);
    }
    public async Task<List<Order>> GetOrdersByUserId(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }
}