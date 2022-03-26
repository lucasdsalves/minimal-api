using demo_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_minimal_api.Data
{
    public class MinimalContextDb : DbContext
    {
        public MinimalContextDb(DbContextOptions<MinimalContextDb> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(p => p.Id);

            modelBuilder.Entity<Product>().Property(p => p.Code).HasColumnType("VARCHAR(15)").IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description).HasColumnType("VARCHAR(200)").IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("DECIMAL").HasPrecision(5,2).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Active).HasColumnType("BIT").IsRequired();
        }
    }
}
