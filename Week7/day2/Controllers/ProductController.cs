using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Route("inventory")]
    public class ProductController : Controller
    {
        static List<dynamic> _products = new List<dynamic>();

        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("manage")]
        public IActionResult ViewIndex()
        {
            ViewBag.ProductList = _products;
            return View();
        }

        [HttpPost("add")]
        public IActionResult AddProduct(string productName, decimal price, int quantity)
        {
            var newProduct = new
            {
                Name = productName,
                Price = price,
                Quantity = quantity,
                Total = price * quantity
            };

            _products.Add(newProduct);

         
            ViewBag.AllProducts = _products;

            return View("Index");
        }

    }
}
