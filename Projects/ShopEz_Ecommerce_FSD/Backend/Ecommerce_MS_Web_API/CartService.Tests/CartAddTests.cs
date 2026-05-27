using CartService.DTOs;
using CartService.Repositories;
using CartService.Services;
using Moq;
using Xunit;

namespace CartService.Tests;

public class CartAddTests
{
    [Fact]
    public async Task AddToCart_throws_when_product_missing()
    {
        var repo = new Mock<ICartRepository>();
        var api = new Mock<IProductApiClient>();
        api.Setup(a => a.GetProduct(It.IsAny<int>())).ReturnsAsync((ProductDto?)null);

        var svc = new CartServices(repo.Object, api.Object);

        var ex = await Assert.ThrowsAsync<Exception>(() =>
            svc.AddToCart(1, new AddToCartDto { ProductId = 99, Quantity = 1 }));

        Assert.Contains("not found", ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}
