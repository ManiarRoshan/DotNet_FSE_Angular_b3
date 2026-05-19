using Microsoft.EntityFrameworkCore;

namespace ContactmgmtWebAPI.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }

        // For Authentication
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Company
            modelBuilder.Entity<Company>().HasData(
                new Company { CompanyId = 1, CompanyName = "Tech Solutions" }
            );

            // Seed Department
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" }
            );
        }
    }
}
