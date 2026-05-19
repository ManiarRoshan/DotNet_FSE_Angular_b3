using Con_Mgmt_Cach_Pag_RateLimiting.Models;
using Microsoft.EntityFrameworkCore;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Repositories
{
    public class ContactRepository: IContactRepository
    {
        private readonly List<Contact>_contacts=new()
            {
        new Contact { ContactId = 1, Name = "John Doe", Email = "john@example.com" },
        new Contact { ContactId = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };
        public async Task<List<Contact>> GetAllAsync()
        {
            await Task.Delay(2000); // Simulate slow DB
            return _contacts;
        }
        public async Task<Contact?> GetByIdAsync(int id)
        {
            await Task.Delay(2000); // Simulate slow DB
            return _contacts.FirstOrDefault(c => c.ContactId == id);
        }
        //Pagination
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<Contact>> GetPaginatedContactsAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Contacts.CountAsync();

            var data = await _context.Contacts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<Contact>(data, totalRecords, pageNumber, pageSize);
        }
    }
}
