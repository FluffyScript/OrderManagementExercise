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

        public async Task<Order?> GetById(Guid id)
        {
            var result = await DbSet.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }
    }
}
