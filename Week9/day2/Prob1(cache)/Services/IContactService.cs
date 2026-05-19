using Con_Mgmt_Cach_Pag_RateLimiting.Models;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Services
{
    public interface IContactService
    {
        Task<List<Contact>> GetAllContactsAsync();
        Task<Contact?> GetContactByIdAsync(int id);
    }
}
