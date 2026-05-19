using System.Text.Json.Serialization;

namespace ContactmgmtWebAPI.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        // Navigation Property
        [JsonIgnore]
        public ICollection<ContactInfo> Contacts { get; set; }
    }
}
