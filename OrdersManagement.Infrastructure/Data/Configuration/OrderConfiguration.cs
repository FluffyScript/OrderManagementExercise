using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain;

namespace OrdersManagement.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.ProductName)
                .HasColumnType("varchar(255)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.DeliveryAddress)
                .HasColumnType("varchar(511)")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
