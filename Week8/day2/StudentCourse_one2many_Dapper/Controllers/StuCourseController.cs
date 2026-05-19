using Microsoft.AspNetCore.Mvc;
using StudentCourse_one2many_Dapper.Models.Repository;

namespace StudentCourse_one2many_Dapper.Controllers
{
    public class StuCourseController : Controller
    {
      private readonly IStudentCourseRepo _repo;
        public StuCourseController(IStudentCourseRepo repo)
        {
            _repo = repo;
        }

            public IActionResult Student()
            {
                var students = _repo.GetStudentsWithCourse();
                return View(students);
            }
        
            public IActionResult Course()
            {
                var courses = _repo.GetCoursesWithStudents();
                return View(courses);
            }
            public IActionResult Index()
            {
                return View();
            }
    }
}
