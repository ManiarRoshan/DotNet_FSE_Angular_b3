using System.ComponentModel.DataAnnotations;
namespace EF_Relationship.Models
{
    public class Dept
    {
        public int DeptId { get; set; }
        public string Dname { get; set; }
        public string Location { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
    
}
