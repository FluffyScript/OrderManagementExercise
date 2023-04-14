using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain;
using OrdersManagement.Domain.Interfaces.Repositories;
using OrdersManagement.Infrastructure.Data.Context;

namespace OrdersManagement.Infrastructure.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Update(Order order)
        {
            DbSet.Update(order);
        }

        public async Task<Order?> GetById(Guid id)
        {
            var result = await DbSet.Include(order => order.Products).FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public override IQueryable<Order> GetAll()
        {
            return DbSet.Include(order => order.Products);
        }
    }
}
