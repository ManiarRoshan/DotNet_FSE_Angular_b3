using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo) => _userRepo = userRepo;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var dtos = users.Select(u => new UserAdminDto
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role
            });
            return Ok(dtos);
        }

        [HttpPut("{id}/toggle-role")]
        public async Task<IActionResult> ToggleRole(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return NotFound(new { message = "User not found" });

            var newRole = user.Role == "Admin" ? "Customer" : "Admin";
            await _userRepo.UpdateRoleAsync(id, newRole);
            return Ok(new { userId = id, role = newRole, message = $"Role updated to {newRole}" });
        }
    }
}
