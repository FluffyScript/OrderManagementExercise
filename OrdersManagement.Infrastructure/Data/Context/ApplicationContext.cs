using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain;
using OrdersManagement.Infrastructure.Data.Configuration;

namespace OrdersManagement.Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.Entity<Order>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
