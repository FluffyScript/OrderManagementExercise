using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain;

namespace OrdersManagement.Infrastructure.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(order => order.Id)
                .HasColumnName("Id");
            try
            {
                builder.HasMany(order => order.Products)
                    .WithOne(product => product.Order)
                    .HasForeignKey(product => product.OrderId);
            } catch(Exception ex)
            {
                throw;
            } 
            builder.Property(order => order.Name)
                .HasColumnType("varchar(255)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(order => order.DeliveryAddress)
                .HasColumnType("varchar(511)")
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
