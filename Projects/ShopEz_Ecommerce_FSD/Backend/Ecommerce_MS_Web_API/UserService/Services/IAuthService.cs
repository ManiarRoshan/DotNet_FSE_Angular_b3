using UserService.Models;

namespace UserService.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
