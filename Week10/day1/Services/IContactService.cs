using ContactMgmtConsoleApp.Models;

namespace ContactMgmtConsoleApp.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAll();  
        void AddContact(Contact contact);

        void UpdateContact(Contact contact);
        void DeleteContact(int id);
    }
}
