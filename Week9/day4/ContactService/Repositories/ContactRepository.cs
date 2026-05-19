using ContactService.Models;
using Microsoft.EntityFrameworkCore;
using ContactService.Repositories;

namespace ContactService.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;
        public ContactRepository(ContactDbContext context)
        { 
            _context = context;
        }
        public async Task<IEnumerable<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<Contact?> GetByIdAsync(int id)
        {
            // Fix: Using AsNoTracking() prevents EF from locking the record
            return await _context.Contacts
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ContactId == id);
        }
        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync (int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if(contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }

        }
    }
}
