using demo_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_minimal_api.Data
{
    public class MinimalContextDb : DbContext
    {
        public MinimalContextDb(DbContextOptions<MinimalContextDb> options) : base(options) { }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MinimalContextDb).Assembly);
        }
    }
}
