using OrdersManagement.Domain.Core.Interfaces.Repositories;

namespace OrdersManagement.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetById(Guid id);
    }
}
