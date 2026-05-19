using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Route("calculator")] // Base: /calculator
    public class CalculatorController : Controller
    {
        [HttpGet("")] // Handles: /calculator (The initial page load)
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("add")] // Handles: /calculator/add (The form submission)
        public IActionResult Index(double num1, double num2)
        {
            double result = num1 + num2;
            ViewData["CalculationResult"] = $"{num1} + {num2} = {result}";
            ViewData["Num1"] = num1;
            ViewData["Num2"] = num2;

            return View();
        }
    }

}




