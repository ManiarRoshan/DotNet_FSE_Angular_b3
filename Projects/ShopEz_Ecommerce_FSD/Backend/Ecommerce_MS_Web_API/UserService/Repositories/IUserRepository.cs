using UserService.Models;

namespace UserService.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateRoleAsync(int userId, string role);
    }
}
