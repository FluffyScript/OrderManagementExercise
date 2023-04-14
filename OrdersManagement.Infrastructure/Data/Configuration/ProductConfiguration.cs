using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain;

namespace OrdersManagement.Infrastructure.Data.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(product => product.Id)
                .HasColumnName("Id");

            builder.Property(product => product.Name)
                .HasColumnType("varchar(255)")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
