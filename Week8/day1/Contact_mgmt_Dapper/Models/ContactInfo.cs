using System.ComponentModel.DataAnnotations;

namespace Contact_Mgmt_Dapper.Models
{
    public class ContactInfo
    {
        public int ContactId { get; set; }

        [Required(ErrorMessage = "First Name is required")]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        [Range(1000000000, 9999999999, ErrorMessage = "Enter valid 10 digit mobile number")]
        public long MobileNo { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        public string Designation { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select company")]
        public int CompanyId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please select department")]
        public int DepartmentId { get; set; }

        public string? CompanyName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
