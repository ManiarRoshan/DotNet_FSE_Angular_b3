using ContactService.Models;
namespace ContactService.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetContacts();
        Task<Contact> GetContactById(int id);
        Task CreateContact(Contact contact);
        Task UpdateContact(Contact contact);
        Task<bool> RemoveContact(int id);

    }
}
