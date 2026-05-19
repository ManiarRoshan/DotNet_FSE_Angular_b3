using EntityFC.Models;
using EntityFC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EntityFC.Controllers
{
    [Route("Contact")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [Route("")]
        [Route("ShowContacts")]
        public IActionResult ShowContacts()
        {
            return View(_contactService.GetAllContacts());
        }
        [Route("Details/{id}")]
        public IActionResult GetContactById(int id)
        { 
            return View(_contactService.GetContactById(id));
        }

        [HttpGet]
        [Route("Add")]
        public IActionResult AddContact()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddContact(ContactInfo contact)
        {
            _contactService.AddContact(contact);
            return RedirectToAction("ShowContacts");
        }
    }
}
