using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Models;
using UserService.Repositories;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IAuthService _authService;

        public AuthController(IUserRepository userRepo, IAuthService authService)
        {
            _userRepo = userRepo;
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("Email already registered");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = PasswordHasher.Hash(dto.Password),
                Role = "Customer"
            };

            await _userRepo.AddAsync(user);
            return Ok(new { message = "User Registered" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAsync(dto.Email);

            if (user == null)
                return Unauthorized("Invalid credentials");

            // Backward compatible: allow existing plain-text rows, prefer hash verification.
            var isValid = PasswordHasher.IsHashedFormat(user.Password)
                ? PasswordHasher.Verify(dto.Password, user.Password)
                : user.Password == dto.Password;

            if (!isValid)
                return Unauthorized("Invalid credentials");

            var token = _authService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
