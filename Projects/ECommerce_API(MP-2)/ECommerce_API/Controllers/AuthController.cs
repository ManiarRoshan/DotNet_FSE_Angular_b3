using Microsoft.AspNetCore.Mvc;
using ECommerce_API.Data;
using ECommerce_API.Models;
using ECommerce_API.Services;
using ECommerce_API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public AuthController(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok("User Registered");
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(user);

            return Ok(token);
        }
    }
}
