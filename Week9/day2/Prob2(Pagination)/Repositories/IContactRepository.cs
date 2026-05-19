using Con_Mgmt_Cach_Pag_RateLimiting.Models;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Repositories
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);

        //pagination 
        Task<PagedResponse<Contact>> GetPaginatedContactsAsync(int pageNumber, int pageSize);
    }
}
