
using EntityFC.Models;

namespace EntityFC.Services
{
    public class ContactService:IContactService
    {
        private static List<ContactInfo> contacts = new List<ContactInfo>();
        public List<ContactInfo> GetAllContacts()
        {
            return contacts;
        }
        public ContactInfo GetContactById(int id) => contacts.FirstOrDefault(c =>c.ContactId == id);
        public void AddContact(ContactInfo contact) => contacts.Add(contact);
    }
}
