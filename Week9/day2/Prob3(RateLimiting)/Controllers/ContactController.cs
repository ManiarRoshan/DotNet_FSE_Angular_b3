using Con_Mgmt_Cach_Pag_RateLimiting.Repositories;
using Con_Mgmt_Cach_Pag_RateLimiting.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //Rate limiting
    [EnableRateLimiting("fixed-policy")] // Add this attribute here
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        //pagination
        private readonly IContactRepository _repository;

        public ContactController(IContactService contactService, IContactRepository repository)
        {
            _contactService = contactService;
            //pagination
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactService.GetAllContactsAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            return contact == null ? NotFound() : Ok(contact);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        //{
        //    // Handle edge cases where user might send 0 or negative numbers
        //    pageNumber = pageNumber < 1 ? 1 : pageNumber;
        //    pageSize = pageSize < 1 ? 5 : pageSize;

        //    var result = await _repository.GetPaginatedContactsAsync(pageNumber, pageSize);
        //    return Ok(result);
        //}


    }
}
