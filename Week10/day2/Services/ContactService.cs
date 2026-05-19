using ContactMgmtConsoleApp.Models;

namespace ContactMgmtConsoleApp.Services
{
    public class ContactService:IContactService
    {
        private readonly List<Contact> _contacts = new();

        public void AddContact(Contact contact)
        {
            // Fixes CA1062: Validate arguments of public methods
            ArgumentNullException.ThrowIfNull(contact);

            Validate(contact);
            _contacts.Add(contact);
        }

        public void UpdateContact(Contact contact)
        {
            ArgumentNullException.ThrowIfNull(contact);

            var existing = _contacts.FirstOrDefault(c => c.Id == contact.Id);
            if (existing == null)
            {
                throw new KeyNotFoundException($"Contact with ID {contact.Id} not found.");
            }

            // Apply updates
            existing.Name = contact.Name;
            existing.Email = contact.Email;
            existing.Phone = contact.Phone;
        }

        public void DeleteContact(int id)
        {
            var contact = _contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                _contacts.Remove(contact);
            }
        }

        // Return as ReadOnly to prevent external modification of the private list
        public IEnumerable<Contact> GetAll() => _contacts.AsReadOnly();

        // REFACTOR: Logic extracted to a private static method to reduce complexity
        private static void Validate(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new ArgumentException("Name cannot be empty.");

            if (!contact.Email.Contains('@'))
                throw new ArgumentException("Invalid email format.");
        }
    }
}
