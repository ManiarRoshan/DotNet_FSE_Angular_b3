using Microsoft.AspNetCore.Mvc;

namespace WebApplication4.Controllers
{
    [Route("students")]
    public class StudentController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("submit")]
        public IActionResult Submit(string studentName, int age, string course)
        {
            TempData["Name"] = studentName;
            TempData["Age"] = age;
            TempData["Course"] = course;

            return RedirectToAction("Display");
        }

        [HttpGet("details")]
        public IActionResult Display()
        {
            ViewBag.StudentName = TempData["Name"];
            ViewData["StudentAge"] = TempData["Age"];
            ViewBag.CourseName = TempData["Course"];

            return View();
        }
    }
}
