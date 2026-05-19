using Contact_Mgmt_Dapper.Models;
using Contact_Mgmt_Dapper.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Contact_Mgmt_Dapper.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepo _repo;
        public ContactController(IContactRepo repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        public IActionResult Details(int id)
        {
            var contactObj = _repo.GetById(id);
            return View(contactObj);
        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadDropdowns();
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(contact);
                return RedirectToAction("Index");
            }

            
            LoadDropdowns();
            ViewBag.ErrorMessage = "Invalid Contact details.";
            return View(contact);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            LoadDropdowns();
            var contactObj = _repo.GetById(id);
            return View(contactObj);
        }

        [HttpPost]
        public IActionResult Edit(ContactInfo contact)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(contact);
                return RedirectToAction("Index");
            }

            LoadDropdowns();
            ViewBag.ErrorMessage = "Invalid Contact details.";
            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contactObj = _repo.GetById(id);
            return View(contactObj);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int ContactId) 
        {
            _repo.Delete(ContactId);
            return RedirectToAction("Index");
        }


        private void LoadDropdowns()
        {
            ViewBag.Companies = new SelectList(
                _repo.GetCompanies(),
                "CompanyId",
                "CompanyName");

            ViewBag.Departments = new SelectList(
                _repo.GetDepartments(),
                "DepartmentId",
                "DepartmentName");
        }

        
        
    }
}
