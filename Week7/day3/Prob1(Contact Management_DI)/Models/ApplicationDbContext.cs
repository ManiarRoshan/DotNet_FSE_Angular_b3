using Microsoft.EntityFrameworkCore;

namespace EntityFC.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
   

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
