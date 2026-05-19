using Microsoft.EntityFrameworkCore;

namespace Con_Mgmt_Cach_Pag_RateLimiting.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // You must provide explicit IDs for seed data in EF Core
            modelBuilder.Entity<Contact>().HasData(
                new Contact { ContactId = 1, Name = "Alice", Email = "alice@test.com", Phone = "111" },
                new Contact { ContactId = 2, Name = "Bob", Email = "bob@test.com", Phone = "222" },
                new Contact { ContactId = 3, Name = "Charlie", Email = "charlie@test.com", Phone = "333" },
                new Contact { ContactId = 4, Name = "David", Email = "david@test.com", Phone = "444" },
                new Contact { ContactId = 5, Name = "Eve", Email = "eve@test.com", Phone = "555" },
                new Contact { ContactId = 6, Name = "Frank", Email = "frank@test.com", Phone = "666" },
                new Contact { ContactId = 7, Name = "Grace", Email = "grace@test.com", Phone = "777" },
                new Contact { ContactId = 8, Name = "Heidi", Email = "heidi@test.com", Phone = "888" },
                new Contact { ContactId = 9, Name = "Ivan", Email = "ivan@test.com", Phone = "999" },
                new Contact { ContactId = 10, Name = "Judy", Email = "judy@test.com", Phone = "000" }
            );
        }
    }
}