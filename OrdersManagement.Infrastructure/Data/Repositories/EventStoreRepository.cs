using OrdersManagement.Domain.Core.Events;
using OrdersManagement.Infrastructure.Data.Context;
using OrdersManagement.Domain.Core.Interfaces.Repositories;

namespace OrdersManagement.Infrastructure.Data.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly ApplicationDbContext _context;

        public EventStoreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StoredEvent> All(Guid id)
        {
            var events = _context.StoredEvents.ToList();
            return events;
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
