using System.ComponentModel.DataAnnotations;

namespace UserService.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        public string Role { get; set; }  // "Admin" or "Customer"
    }
}
