using OrdersManagement.Domain.Core.Events;
using OrdersManagement.Domain.Core.Interfaces.Repositories;
using System.Text.Json;

namespace OrdersManagement.Infrastructure.Data
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData);

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
