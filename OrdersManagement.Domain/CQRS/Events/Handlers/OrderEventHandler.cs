using MediatR;

namespace OrdersManagement.Domain.CQRS.Events.Handlers
{
    public class OrderEventHandler :
        INotificationHandler<OrderCreated>,
        INotificationHandler<OrderUpdated>,
        INotificationHandler<OrderCanceled>
    {
        public Task Handle(OrderCreated message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdated message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderCanceled message, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
