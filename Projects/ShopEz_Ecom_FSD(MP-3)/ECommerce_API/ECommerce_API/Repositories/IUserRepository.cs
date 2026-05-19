using ECommerce_API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
    }

}