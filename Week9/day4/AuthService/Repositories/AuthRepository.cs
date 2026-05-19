using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;
        public AuthRepository(AuthDbContext context) => _context = context;

        public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
