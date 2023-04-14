using OrdersManagement.Domain.Interfaces.Repositories;
using OrdersManagement.Domain;
using OrdersManagement.Infrastructure.Data.Context;

namespace OrdersManagement.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
