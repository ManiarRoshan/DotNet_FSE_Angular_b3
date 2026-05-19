using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class EmpController : Controller
    {
        public IActionResult Index()
        {
            List<Employee> empObj = new List<Employee>();

            Employee empObj1 = new Employee()
            {
                Empno = 1,
                Ename = "viju",
                Job = "CEO",
                Salary = 100000,
                Deptno = 10
            };
            Employee empObj2 = new Employee()
            {
                Empno = 1,
                Ename = "rakesh",
                Job = "Developer",
                Salary = 100000,
                Deptno = 10
            };

            empObj.Add(empObj1);
            empObj.Add(empObj2);

            return View(empObj);
        }
    }
}
