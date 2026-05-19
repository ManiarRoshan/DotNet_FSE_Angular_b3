namespace EF_Relationship.Models
{
    public class Course
    {
        public int CourseId { get; set; }//primary
        public string CourseName {  get; set; }

        public ICollection<Student> Students { get; set; }//Nav Prop
    }
}
