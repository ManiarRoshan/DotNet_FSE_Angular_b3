using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.DTOs;
using UserService.Repositories;

namespace UserService.Controllers
{
    [Authorize]   
    [Route("api/user")] 
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserProfileController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                return Unauthorized("Invalid user");

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                return NotFound();

            var profile = new UserProfileDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
            return Ok(profile);
        }
    }
}