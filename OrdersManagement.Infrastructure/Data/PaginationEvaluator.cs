using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Interfaces;

namespace OrdersManagement.Infrastructure.Data
{
    public class PaginationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IPagination<T> pagination)
        {
            var query = inputQuery;

            if (pagination.Criteria != null)
            {
                query = query.Where(pagination.Criteria);
            }

            query = pagination.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            query = pagination.IncludeStrings.Aggregate(query,
                                    (current, include) => current.Include(include));

            if (pagination.OrderBy != null)
            {
                query = query.OrderBy(pagination.OrderBy);
            }
            else if (pagination.OrderByDescending != null)
            {
                query = query.OrderByDescending(pagination.OrderByDescending);
            }

            if (pagination.GroupBy != null)
            {
                query = query.GroupBy(pagination.GroupBy).SelectMany(x => x);
            }

            if (pagination.IsPagingEnabled)
            {
                query = query.Skip(pagination.Skip)
                             .Take(pagination.Take);
            }

            return query;
        }
    }
}
