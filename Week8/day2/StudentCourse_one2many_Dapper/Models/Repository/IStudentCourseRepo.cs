namespace StudentCourse_one2many_Dapper.Models.Repository
{
    public interface IStudentCourseRepo
    {
        IEnumerable<Student> GetStudentsWithCourse();
        IEnumerable<Course> GetCoursesWithStudents();
    }
}
