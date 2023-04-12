using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Domain.CQRS.Events
{
    public class OrderCreated : Event
    {
        public OrderCreated(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
