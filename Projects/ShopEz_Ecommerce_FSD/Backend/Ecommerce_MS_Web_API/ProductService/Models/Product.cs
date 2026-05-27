using System.ComponentModel.DataAnnotations;

namespace ProductService.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Only letters and numbers allowed")]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Range(0, 1000)]
        public int Stock { get; set; }
        public string Category { get; set; }
        public bool IsDeleted { get; set; }
    }
}