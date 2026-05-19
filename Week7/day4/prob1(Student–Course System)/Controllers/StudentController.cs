using EF_Relationship.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EF_Relationship.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Students = _context.Students
                                  .Include(c => c.Courses)
                                  .ToList();
            return View(Students);
        }
      

    }
}
