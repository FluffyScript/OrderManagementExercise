using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Domain.CQRS.Events
{
    public class OrderCanceled : Event
    {
        public OrderCanceled(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
