using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;

namespace ProductService.Controllers;

[ApiController]
[Route("api/images")]
[Authorize(Roles = "Admin")]
public class ImagesController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(IWebHostEnvironment env, ILogger<ImagesController> logger)
    {
        _env = env;
        _logger = logger;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] ProductImageUploadDto dto)
    {
        if (dto.Image == null || dto.Image.Length == 0)
            return BadRequest("No image file provided");

        // Allowed extensions
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var ext = Path.GetExtension(dto.Image.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(ext))
            return BadRequest("Invalid file type. Allowed: jpg, jpeg, png, gif, webp");

        // Max size 5MB
        if (dto.Image.Length > 5 * 1024 * 1024)
            return BadRequest("File too large. Max 5MB");

        // Generate unique filename
        var fileName = $"{Guid.NewGuid():N}{ext}";
        var imagesFolder = Path.Combine(_env.WebRootPath, "images");
        if (!Directory.Exists(imagesFolder))
            Directory.CreateDirectory(imagesFolder);

        var filePath = Path.Combine(imagesFolder, fileName);
        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.Image.CopyToAsync(stream);
        }

        // Return the URL path that will be stored in DB
        var imageUrl = $"/images/{fileName}";
        _logger.LogInformation("Image uploaded: {ImageUrl}", imageUrl);
        return Ok(new { imageUrl });
    }
}