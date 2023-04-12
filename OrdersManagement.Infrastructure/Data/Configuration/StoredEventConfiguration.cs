using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Infrastructure.Data.Configuration
{
    public class StoredEventConfiguration : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Data)
                .HasColumnType("varchar()")
                .IsRequired();

            builder.Property(c => c.Timestamp)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
