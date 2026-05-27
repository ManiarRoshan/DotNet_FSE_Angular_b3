using Microsoft.Extensions.Configuration;
using Moq;
using UserService.Models;
using UserService.Services;
using Xunit;

namespace UserService.Tests;

public class AuthServiceTests
{
    [Fact]
    public void GenerateToken_returns_non_empty_jwt()
    {
        var cfg = new Mock<IConfiguration>();
        cfg.Setup(c => c["Jwt:Key"]).Returns("SuperSecretKeyForShopEZProject2026!CheckLength");
        cfg.Setup(c => c["Jwt:Issuer"]).Returns("ShopEZ");
        cfg.Setup(c => c["Jwt:Audience"]).Returns("ShopEZUsers");

        var auth = new AuthService(cfg.Object);

        var token = auth.GenerateToken(new User
        {
            UserId = 42,
            Email = "buyer@example.com",
            Role = "Customer",
            Name = "Test User",
            Password = "not-used-for-token"
        });

        Assert.False(string.IsNullOrWhiteSpace(token));
        Assert.Contains('.', token);
    }
}
