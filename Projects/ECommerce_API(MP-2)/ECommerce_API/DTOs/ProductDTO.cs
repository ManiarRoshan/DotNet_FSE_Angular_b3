using System.ComponentModel.DataAnnotations;

public class ProductDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Range(1, 1000000)]
    public decimal Price { get; set; }

    public string ImageUrl { get; set; }

    [Range(0, 1000)]
    public int Stock { get; set; }
}