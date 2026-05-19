using ContactmgmtWebAPI.Models;
using ContactmgmtWebAPI.Models.Data;
using Microsoft.EntityFrameworkCore;


namespace ContactmgmtWebAPI.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ContactInfo>> GetAllContactsAsync()
        {
            // .Include is used to fetch Company and Department details (Navigation Properties)
            return await _context.Contacts
                .Include(c => c.Company)
                .Include(c => c.Department)
                .ToListAsync();
        }
        public async Task<ContactInfo> GetContactByIdAsync(int id)
        {
            return await _context.Contacts
                .Include(c => c.Company)
                .Include(c => c.Department)
                .FirstOrDefaultAsync(c => c.ContactId == id);
        }

        public async Task AddContactAsync(ContactInfo contact)
        {
            // Safety: Set the objects to null so EF Core doesn't try to create 
            // a new Company when you just wanted to link an existing ID.
            contact.Company = null;
            contact.Department = null;

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateContactAsync(ContactInfo contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
