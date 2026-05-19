using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
      public static List<Product> products = new List<Product>
        {
              new Product { Id = 1, Name = "Laptop", Price = 50000, Category = "Electronics" },
              new Product { Id = 2, Name = "Mobile", Price = 20000, Category = "Electronics" },
              new Product { Id = 3, Name = "Shoes", Price = 3000, Category = "Fashion" }
        };

        // Index → List of products
        public IActionResult Index()
        {

            return View(products);
        }
        //  Details → Single product
        public IActionResult Details(int id)
        {
            var filteredProduct = products.FirstOrDefault(p => p.Id == id);
            return View(filteredProduct);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product productObj)
        {

            products.Add(productObj);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var productDetails = products.Find(p => p.Id == id);


            return View(productDetails);

        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            var deleteProduct = products.Find(p => p.Id == id);
            products.Remove(deleteProduct);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var filteredProduct = products.FirstOrDefault(p => p.Id == id);
            return View(filteredProduct);


        }
        [HttpPost]

        public IActionResult Edit(Product obj)
        {
            var exisitingProduct = products.Find(p => p.Id == obj.Id);

            products.Remove(exisitingProduct);
            products.Add(obj);
            return RedirectToAction("Index");

        }

    }
}