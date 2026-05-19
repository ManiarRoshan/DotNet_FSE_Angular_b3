using ContactService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactService.Models;

namespace ContactService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;
        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await _service.GetContacts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            var contact = await _service.GetContactById(id);
            if (contact == null)
            {
                return NotFound("Requested product does not exists");
            }
            else
            {
                return Ok(contact);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Post(Contact contact)
        {
            await _service.CreateContact(contact);
            return Ok(new { contact, status = "New contact successfully added to server..!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, Contact contact)
        {
            if (id != contact.ContactId)
            {
                return BadRequest("Requested contact Id mismatch.");
            }

            // This call now uses AsNoTracking, so it won't conflict with the update
            var existing = await _service.GetContactById(id);
            if (existing == null)
            {
                return NotFound("Requested Contact does not exist");
            }

            try
            {
                await _service.UpdateContact(contact);
                return Ok(new
                {
                    updateContact = contact,
                    status = "Contact details are updated successfully in server..!"
                });
            }
            catch (Exception ex)
            {
                // This will catch any remaining issues and tell you exactly what they are
                return StatusCode(500, $"Update failed: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool flag = await _service.RemoveContact(id);

            if (flag == false)
            {
                return NotFound("Requested Contact does not exists");
            }
            else
            {
                return Ok(new { status = "Contact details are deleted successfully in server..!" });
            }
        }
    }
}
