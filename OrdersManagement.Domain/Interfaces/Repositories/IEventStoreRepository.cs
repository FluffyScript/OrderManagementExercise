using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Domain.Interfaces.Repositories
{
    public interface IEventStoreRepository
    {
        IList<StoredEvent> All();

        void Store(StoredEvent theEvent);
    }
}
