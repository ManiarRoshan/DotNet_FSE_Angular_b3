using Contact_Mgmt_Dapper.Controllers;
using Contact_Mgmt_Dapper.Models;
using Contact_Mgmt_Dapper.Models.Repositories;
using Dapper;
using Microsoft.Data.SqlClient;


namespace Contact_Mgmt_Dapper.Repositories
{
    public class ContactRepo : IContactRepo
    {
        private readonly string _connStr;
        public ContactRepo(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection");
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connStr);
        }
        public IEnumerable<ContactInfo> GetAll()
        {
            string sqlQuery= @"SELECT c.*, comp.CompanyName, dept.DepartmentName
                                FROM ContactInfo c
                                JOIN Company comp ON c.CompanyId = comp.CompanyId
                                JOIN Department dept ON c.DepartmentId = dept.DepartmentId";
            var db = GetConnection();
            return db.Query<ContactInfo>(sqlQuery);
        }
        public ContactInfo GetById(int id)
        {
            string sqlQuery = @"SELECT c.*, comp.CompanyName, dept.DepartmentName
                                FROM ContactInfo c
                                JOIN Company comp ON c.CompanyId = comp.CompanyId
                                JOIN Department dept ON c.DepartmentId = dept.DepartmentId
                                WHERE ContactId=@Id";

            var db = GetConnection();
            return db.QueryFirstOrDefault<ContactInfo>(sqlQuery, new { Id = id });
        }
        public void Add(ContactInfo contact)
        {
            string sqlQuery = @"INSERT INTO ContactInfo
                                (FirstName, LastName, EmailId, MobileNo, Designation, CompanyId, DepartmentId)
                                VALUES
                                (@FirstName, @LastName, @EmailId, @MobileNo, @Designation, @CompanyId, @DepartmentId)";

            var db = GetConnection();
            db.Execute(sqlQuery, contact);
        }
        public void Update(ContactInfo contact)
        {
            string sqlQuery = @"UPDATE ContactInfo
                                SET FirstName=@FirstName,
                                    LastName=@LastName,
                                    EmailId=@EmailId,
                                    MobileNo=@MobileNo,
                                    Designation=@Designation,
                                    CompanyId=@CompanyId,
                                    DepartmentId=@DepartmentId
                                WHERE ContactId=@ContactId";

            var db = GetConnection();
            db.Execute(sqlQuery, contact);
        }
        public void Delete(int id)
        {
            string sqlQuery = "DELETE FROM ContactInfo WHERE ContactId=@Id";
            var db = GetConnection();
            db.Execute(sqlQuery, new { Id = id });
        }

        public IEnumerable<Company> GetCompanies()
        {
            var db = GetConnection();
            return db.Query<Company>("SELECT * FROM Company");
        }

        public IEnumerable<Department> GetDepartments()
        {
            var db = GetConnection();
            return db.Query<Department>("SELECT * FROM Department");
        }
    }
}
