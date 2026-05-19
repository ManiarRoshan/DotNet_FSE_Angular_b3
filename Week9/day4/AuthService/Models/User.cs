using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class User
    {
        [Key] 
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
    }
    public class LoginRequest
    {
        // DTO for Login Request
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
