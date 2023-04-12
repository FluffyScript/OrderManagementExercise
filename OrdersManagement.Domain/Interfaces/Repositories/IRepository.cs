using OrdersManagement.Domain.Interfaces;

namespace OrdersManagement.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task AddAsync(TEntity obj);
        Task<TEntity?> GetByIdAsync(Guid id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(IPagination<TEntity> spec);
        IQueryable<TEntity> GetAllSoftDeleted();
        void Update(TEntity obj);
        Task RemoveAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
