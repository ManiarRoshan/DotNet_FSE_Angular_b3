namespace WebAPI_demo.Models.DataAccess
{
    public interface IContactRepository
    {
        IEnumerable<ContactInfo> GetAllContacts();
        ContactInfo? GetContactById(int id);
        ContactInfo AddContact(ContactInfo contact);
        bool UpdateContact(int id, ContactInfo contact);
        bool DeleteContact(int id);
    }
}
