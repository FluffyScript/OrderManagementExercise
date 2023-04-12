using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Domain.CQRS.Events
{
    public class OrderUpdated : Event
    {
        public OrderUpdated(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
