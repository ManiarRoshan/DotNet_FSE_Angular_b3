using AuthService.Models;

namespace AuthService.Repositories
{
    public interface IAuthRepository
    {
        Task Register(User user);
        Task<User?> GetUserByEmail(string email);
    }
}
