using Contact_Mgmt_Dapper.Controllers;

namespace Contact_Mgmt_Dapper.Models.Repositories
{
    public interface IContactRepo
    {
        IEnumerable<ContactInfo> GetAll();
        ContactInfo GetById(int id);
        void Add(ContactInfo Contact);
        void Update(ContactInfo Contact);
        void Delete(int id);
        IEnumerable<Company> GetCompanies();
        IEnumerable<Department> GetDepartments();

    }
}
