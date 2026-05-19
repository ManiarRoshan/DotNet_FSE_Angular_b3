using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_demo.Models;
using WebAPI_demo.Models.DataAccess;

namespace WebAPI_demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repository;

        // Constructor Injection
        public ContactController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetContacts()
        {
            var result = _repository.GetAllContacts();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _repository.GetContactById(id);
            if (contact == null)
            {
                return NotFound("Requested contact does not exist");
            }
            return Ok(contact);
        }

        //[HttpPost]
        //public IActionResult CreateContact(ContactInfo contact)
        //{
        //    if (contact == null) return BadRequest("Invalid contact data");

        //    var newContact = _repository.AddContact(contact);
        //    return Ok(new { data = newContact, status = "New contact successfully added to server..!" });
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateContact(int id, ContactInfo contact)
        //{
        //    var isUpdated = _repository.UpdateContact(id, contact);

        //    if (!isUpdated)
        //    {
        //        return NotFound("Requested contact does not exist");
        //    }

        //    return Ok(new { status = "Contact details are updated successfully in server..!" });
        //}
        [HttpPost]
        public IActionResult CreateContact(ContactInfo contact) // Added [FromBody]
        {
            if (contact == null) return BadRequest("Invalid data");
            var newContact = _repository.AddContact(contact);
            return Ok(new { data = newContact, status = "Success" });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, ContactInfo contact) // Added [FromBody]
        {
            var isUpdated = _repository.UpdateContact(id, contact);
            if (!isUpdated) return NotFound("Not Found");
            return Ok(new { status = "Updated" });
         }

         [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var isDeleted = _repository.DeleteContact(id);

            if (!isDeleted)
            {
                return NotFound("Requested contact does not exist");
            }

            return Ok(new { status = "Contact details are deleted successfully from server..!" });
        }
    }
}
