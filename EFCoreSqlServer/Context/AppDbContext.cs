using EFCoreSqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlServer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
    }
}
