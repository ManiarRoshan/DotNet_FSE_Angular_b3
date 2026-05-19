using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>
        {
            new Product() { Id = 1, Name = "Laptop", Price = 99999.99, Category = "Electronics" },
            new Product() { Id = 2, Name = "Smartphone", Price = 49999.99, Category = "Electronics" },
            new Product() { Id = 3, Name = "Table", Price = 4999.99, Category = "Furniture" },
            new Product() { Id = 4, Name = "Chair", Price = 999.99, Category = "Furniture" },
            new Product() { Id = 5, Name = "Headphones", Price = 1499.99, Category = "Electronics" }
        };

        //List of all products
        public IActionResult Index()
        {
            return View(products);
        }

        // Details of a specific product
        public IActionResult Details(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return View(product);
        }
    }
}