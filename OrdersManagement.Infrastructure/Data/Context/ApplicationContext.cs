using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain;
using OrdersManagement.Domain.Core.Events;
using OrdersManagement.Infrastructure.Data.Configuration;

namespace OrdersManagement.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StoredEvent> StoredEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StoredEventConfiguration());
            modelBuilder.Entity<StoredEvent>();
            modelBuilder.Entity<Order>();
            modelBuilder.Entity<Product>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
