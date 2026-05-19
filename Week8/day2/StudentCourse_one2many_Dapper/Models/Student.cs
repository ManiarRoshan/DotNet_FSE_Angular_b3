using System.ComponentModel.DataAnnotations.Schema;

namespace StudentCourse_one2many_Dapper.Models
{
    public class Student
    {
        public int StudentId {  get; set; }
        public string StudentName { get; set; }
        
        public int CourseId {  get; set; }
        public Course Course { get; set; }
    }
}
