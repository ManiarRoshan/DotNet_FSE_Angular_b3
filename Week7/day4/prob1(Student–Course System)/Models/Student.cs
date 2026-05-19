using System.ComponentModel.DataAnnotations.Schema;

namespace EF_Relationship.Models
{
    public class Student
    {
        public int StudentID {  get; set; }
        public string StudentName { get; set; }

        [ForeignKey("CourseId")]
        public int CourseId {  get; set; }//Foreign
        public Course Courses { get; set; }//Nav Prop

  }
}
