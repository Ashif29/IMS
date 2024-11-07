

using IMS.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace IMS.EntityFrameworkCore.Data
{
    public class AppDbContext : DbContext
    {
        //basic tables
        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
