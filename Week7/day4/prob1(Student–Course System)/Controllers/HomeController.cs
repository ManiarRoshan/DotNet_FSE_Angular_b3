
using EF_Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace EF_Relationship.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Emps()
        {
            var emps = _context.Employees.Include(e => e.Dept).ToList();
            return View(emps);
        }

        public IActionResult Depts()
        {
            var depts = _context.Depts.Include(d => d.Employees).ToList();
            return View(depts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
