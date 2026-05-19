using System.ComponentModel.DataAnnotations;

namespace ECommerce_API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }
        public string Role { get; set; }

        public List<Order> Orders { get; set; }
    }
}