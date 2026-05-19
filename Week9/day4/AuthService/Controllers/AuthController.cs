using AuthService.Models;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthRepository repo, ITokenService tokenService)
        {
            _repo = repo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            await _repo.Register(user);
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _repo.GetUserByEmail(request.Email);
            if (user == null || user.Password != request.Password)
                return Unauthorized("Invalid credentials");

            var token = _tokenService.GenerateToken(user.Email, user.Role);
            return Ok(new { Token = token });
        }
    }
}
