using demo_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

namespace demo_minimal_api.Data.EF.Configuration
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Products>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("Products");

            builder.Property(p => p.Code).HasColumnType("VARCHAR(15)").IsRequired();
            builder.Property(p => p.Description).HasColumnType("VARCHAR(200)").IsRequired();
            builder.Property(p => p.Price).HasColumnType("DECIMAL").HasPrecision(5, 2).IsRequired();
            builder.Property(p => p.Active).HasColumnType("BIT").IsRequired();
        }
    }
}
