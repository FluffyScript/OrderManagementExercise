using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Core.Interfaces.Repositories;
using OrdersManagement.Domain.Interfaces;
using OrdersManagement.Infrastructure.Data.Context;

namespace OrdersManagement.Infrastructure.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var result = await DbSet.FindAsync(id);
            return result;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual IQueryable<TEntity> GetAll(IPagination<TEntity> pagination)
        {
            return PaginationEvaluator<TEntity>.GetQuery(DbSet.AsQueryable(), pagination);
        }

        public virtual IQueryable<TEntity> GetAllSoftDeleted()
        {
            return DbSet.IgnoreQueryFilters()
                .Where(e => EF.Property<bool>(e, "IsDeleted") == true);
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual async Task RemoveAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if(item == null)
            {
                throw new Exception($"Item not found for id:{id}");
            }

            DbSet.Remove(item);
        }

        public async Task<int> SaveChangesAsync()
        {
            var result = await Db.SaveChangesAsync();
            return result;
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
