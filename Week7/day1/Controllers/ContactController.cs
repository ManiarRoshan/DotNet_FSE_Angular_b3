using Microsoft.AspNetCore.Mvc;
using ContactManagementWebApplication.Models;


namespace ContactManagementWebApplication.Controllers
{
    public class ContactController : Controller
    {
        private static List<ContactInfo> _contacts = new List<ContactInfo>
        {
            new ContactInfo {ContactId = 1,FirstName = "b",LastName = "c",
                CompanyName = "ggle",EmailId = "bc.ggle.com",MobileNo = 9876543210,
                Designation = "Manager"}
        };

        // 1. Show all contacts
        public ActionResult ShowContacts()
        {
            return View(_contacts);
        }

        // 2. Search contact by ID
        public ActionResult GetContactById(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // 3. GET: AddContact (Show Form)
        public ActionResult AddContact()
        {
            return View();
        }

        // 4. POST: AddContact (Process Form)
        [HttpPost]
        public ActionResult AddContact(ContactInfo contactInfo)
        {
            _contacts.Add(contactInfo);
            return RedirectToAction("ShowContacts");
        }
    }
    
}
