using OrdersManagement.Domain.Core.Interfaces.Repositories;

namespace OrdersManagement.Domain.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        public IQueryable<Product> GetProductsByOrder(Guid orderId);
    }
}
