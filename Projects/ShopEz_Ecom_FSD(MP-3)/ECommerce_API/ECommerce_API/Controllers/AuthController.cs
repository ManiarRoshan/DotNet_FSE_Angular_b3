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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());
            if (existingUser != null)
                return BadRequest("Email already registered");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = "Customer"   // force role, ignore client value
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User Registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // Case‑insensitive email comparison
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower() && u.Password == dto.Password);

            if (user == null)
            {
                Console.WriteLine($"Login failed for email: {dto.Email}");
                return Unauthorized("Invalid credentials");
            }

            var token = _authService.GenerateToken(user);
            return Ok(token);
        }
    }
}