using Con_Mgmt_Cach_Pag_RateLimiting.Models;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Repositories
{
    public class ContactRepository: IContactRepository
    {
        private readonly List<Contact>_contacts=new()
            {
        new Contact { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new Contact { Id = 2, Name = "Jane Smith", Email = "jane@example.com" }
        };
        public async Task<List<Contact>> GetAllAsync()
        {
            await Task.Delay(2000); // Simulate slow DB
            return _contacts;
        }
        public async Task<Contact?> GetByIdAsync(int id)
        {
            await Task.Delay(2000); // Simulate slow DB
            return _contacts.FirstOrDefault(c => c.Id == id);
        }
    }
}
