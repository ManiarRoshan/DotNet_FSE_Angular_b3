using WebAPI_demo.Models;

namespace WebAPI_demo.Models.DataAccess
{
    public class ContactRepository:IContactRepository
    {
        private static List<ContactInfo> contacts = new List<ContactInfo>();

        public IEnumerable<ContactInfo> GetAllContacts()
        {
            return contacts;
        }

        public ContactInfo? GetContactById(int id)
        {
            return contacts.FirstOrDefault(c => c.ContactId == id);
        }

        public ContactInfo AddContact(ContactInfo contact)
        {
            // Auto-generate ContactId
            contact.ContactId = contacts.Any() ? contacts.Max(c => c.ContactId) + 1 : 1;
            contacts.Add(contact);
            return contact;
        }

        public bool UpdateContact(int id, ContactInfo contact)
        {
            var existing = contacts.FirstOrDefault(c => c.ContactId == id);
            if (existing == null) return false;

            existing.FirstName = contact.FirstName;
            existing.LastName = contact.LastName;
            existing.EmailId = contact.EmailId;
            existing.MobileNo = contact.MobileNo;
            existing.Designation = contact.Designation;
            existing.CompanyId = contact.CompanyId;
            existing.DepartmentId = contact.DepartmentId;

            return true;
        }

        public bool DeleteContact(int id)
        {
            var contact = contacts.FirstOrDefault(c => c.ContactId == id);
            if (contact == null) return false;

            contacts.Remove(contact);
            return true;
        }
    }
}
