namespace ProductService.DTOs;

public class ProductImageUploadDto
{
    public IFormFile Image { get; set; } = null!;
}