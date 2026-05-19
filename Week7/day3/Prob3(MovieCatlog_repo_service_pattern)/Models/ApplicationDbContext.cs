using Microsoft.EntityFrameworkCore;

namespace MovieCatlog_repo_service_pattern.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        { }

        public DbSet<Movie> Movies { get; set; }
    }
}
