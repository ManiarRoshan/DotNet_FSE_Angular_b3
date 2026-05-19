using ContactmgmtWebAPI.Models;
using ContactmgmtWebAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactmgmtWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _repository.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _repository.GetContactByIdAsync(id);
            if (contact == null) 
                return NotFound();
            return Ok(contact);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ContactInfo contact) // Added [FromBody]
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                await _repository.AddContactAsync(contact);
                return CreatedAtAction(nameof(GetById), new { id = contact.ContactId }, contact);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, ContactInfo contact)
        {
            if (id != contact.ContactId) return BadRequest();
            await _repository.UpdateContactAsync(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteContactAsync(id);
            return NoContent();
        }
    }
}
