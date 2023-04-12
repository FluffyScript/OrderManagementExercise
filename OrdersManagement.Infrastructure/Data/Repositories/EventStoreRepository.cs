using OrdersManagement.Domain.Core.Events;
using OrdersManagement.Domain.Core.Interfaces.Repositories;
using OrdersManagement.Infrastructure.Data.Context;

namespace OrdersManagement.Infrastructure.Data.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public EventStoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid aggregateId)
        {
            return (from e in _context.StoredEvents where e.AggregateId == aggregateId select e).ToList();
        }

        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvents.Add(theEvent);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
