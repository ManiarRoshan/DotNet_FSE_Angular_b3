using Microsoft.Data.SqlClient;
using StudentCourse_one2many_Dapper.Models;
using StudentCourse_one2many_Dapper.Models.Repository;
using System.Data;
using Dapper;

namespace StudentCourse_one2many_Dapper.Models.Repository
{
    public class StudentCourseRepo:IStudentCourseRepo
    {
        private readonly string _conn;
        public StudentCourseRepo(IConfiguration config)
        {
            _conn = config.GetConnectionString("DefaultConnection");
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_conn);
        }
        public IEnumerable<Student> GetStudentsWithCourse()
        {
            using (var db = GetConnection())
            {
                string sql = @"SELECT s.StudentId, s.StudentName, s.CourseId,
                           c.CourseId, c.CourseName 
                           FROM Students s 
                           INNER JOIN Courses c ON s.CourseId = c.CourseId";
                return db.Query<Student, Course, Student>(sql, (student, course) =>
                {
                    student.Course = course;
                    return student;
                }, 
                splitOn: "CourseId"
                );
                
            };
        }
        public IEnumerable<Course> GetCoursesWithStudents()
        {
            using (var db = GetConnection())
            {
                string sql = @"SELECT
                               c.CourseId, c.CourseName,
                               s.StudentId, s.StudentName, s.CourseId
                               FROM Courses c
                               LEFT JOIN Students s
                               ON c.CourseId = s.CourseId";
                var dictObj = new Dictionary<int, Course>();
                var list = db.Query<Course, Student, Course>(
                    sql,(course, student) =>
                    {
                        if (!dictObj.TryGetValue(course.CourseId, out var currentCourse))
                        {
                            currentCourse = course;
                            currentCourse.Students = new List<Student>();
                            dictObj.Add(currentCourse.CourseId, currentCourse);
                        }

                        if (student != null && student.StudentId != 0)
                        {
                            currentCourse.Students.Add(student);
                        }

                        return currentCourse;
                    },
                    splitOn: "StudentId"
                );

                return dictObj.Values;
            }
        }

    }
}
