using EF_Relationship.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Relationship.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Dept> Depts { get; set; }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                        .HasOne(e => e.Dept)
                        .WithMany(d => d.Employees)
                        .HasForeignKey(e => e.DeptId);
        

       
        
            modelBuilder.Entity<Student>()
                        .HasOne(s => s.Courses)
                        .WithMany(c => c.Students)
                        .HasForeignKey(e => e.CourseId);
        }

    }
}