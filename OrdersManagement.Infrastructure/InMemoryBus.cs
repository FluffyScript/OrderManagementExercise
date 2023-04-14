using MediatR;
using OrdersManagement.Domain.Core;
using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Infrastructure
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IEventStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            _eventStore?.Save(@event);
            return _mediator.Publish(@event);
        }
    }
}