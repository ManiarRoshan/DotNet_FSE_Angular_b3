using System.Text.Json.Serialization;

namespace ContactmgmtWebAPI.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        // Navigation Property
        [JsonIgnore]
        public ICollection<ContactInfo> Contacts { get; set; }
    }
}
