using Con_Mgmt_Cach_Pag_RateLimiting.Models;
using Con_Mgmt_Cach_Pag_RateLimiting.Repositories;
using Microsoft.Extensions.Caching.Memory;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IMemoryCache _cache;
        private const string AllContactsCacheKey = "AllContactsList";

        public ContactService(IContactRepository repository, IMemoryCache cache)
        {
            _repository = repository;
            _cache = cache;
        }
        public async Task<List<Contact>> GetAllContactsAsync()
        {
            // Try to get data from cache
            if (!_cache.TryGetValue(AllContactsCacheKey, out List<Contact>? contacts))
            {
                // If not in cache, fetch from Repo
                contacts = await _repository.GetAllAsync();

                // Set cache options (60 seconds)
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

                // Save to cache
                _cache.Set(AllContactsCacheKey, contacts, cacheOptions);
            }
            return contacts!;
        }

        public async Task<Contact?> GetContactByIdAsync(int id)
        {
            string cacheKey = $"Contact_{id}";

            if (!_cache.TryGetValue(cacheKey, out Contact? contact))
            {
                contact = await _repository.GetByIdAsync(id);

                if (contact != null)
                {
                    _cache.Set(cacheKey, contact, TimeSpan.FromSeconds(60));
                }
            }
            return contact;
        }
    }
}
