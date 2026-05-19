using ECommerce_API.Models;

namespace ECommerce_API.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
