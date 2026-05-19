using ContactmgmtWebAPI.Models;

namespace ContactmgmtWebAPI.Repositories
{
    public interface IContactRepository
    {
        Task<IEnumerable<ContactInfo>> GetAllContactsAsync();
        Task<ContactInfo> GetContactByIdAsync(int id);
        Task AddContactAsync(ContactInfo contact);
        Task UpdateContactAsync(ContactInfo contact);
        Task DeleteContactAsync(int id);
    }
}
