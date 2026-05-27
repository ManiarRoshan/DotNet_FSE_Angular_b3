using Moq;
using ProductService.Models;
using ProductService.Repositories;
using ProductService.Services;
using Xunit;

namespace ProductService.Tests;

public class ProductCatalogTests
{
    [Fact]
    public async Task GetProducts_returns_empty_when_repository_empty()
    {
        var read = new Mock<IProductReadRepository>();
        read.Setup(r => r.GetAllProducts()).ReturnsAsync(Array.Empty<Product>());
        var write = new Mock<IProductWriteRepository>();

        var svc = new ProductServices(read.Object, write.Object);
        var list = await svc.GetProducts();

        Assert.Empty(list);
    }
}
