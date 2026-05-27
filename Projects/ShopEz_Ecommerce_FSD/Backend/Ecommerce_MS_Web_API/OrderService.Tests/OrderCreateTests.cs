using Moq;
using OrderService.DTOs;
using OrderService.Repositories;
using OrderService.Services;
using Xunit;

namespace OrderService.Tests;

public class OrderCreateTests
{
    [Fact]
    public async Task CreateOrder_throws_when_cart_is_empty()
    {
        var repo = new Mock<IOrderRepository>();
        var products = new Mock<IProductApiClient>();
        var svc = new OrderService.Services.OrderService(repo.Object, products.Object);

        var dto = new OrderDTO
        {
            UserId = 1,
            ShippingAddress = "Test address",
            PaymentMethod = "cod",
            Items = new List<OrderItemDTO>()
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => svc.CreateOrder(dto));
        Assert.Contains("empty", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}
