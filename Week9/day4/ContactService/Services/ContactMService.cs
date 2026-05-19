using ContactService.Models;
using ContactService.Repositories;

namespace ContactService.Services
{
    public class ContactMService : IContactService
    {
        private readonly IContactRepository _repository;
        public ContactMService(IContactRepository contactRepository)
        {
            _repository = contactRepository;
        }
        public async Task<IEnumerable<Contact>> GetContacts()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Contact> GetContactById(int id) => await _repository.GetByIdAsync(id);
        public async Task CreateContact(Contact contact) => await _repository.AddAsync(contact);
        public async Task UpdateContact(Contact contact) => await _repository.UpdateAsync(contact);
        public async Task<bool> RemoveContact(int id)
        {
            var exists = await _repository.GetByIdAsync(id);
            if (exists == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
