using MediatR;
using OrdersManagement.Domain.Core.Events;

namespace OrdersManagement.Domain.Core
{
    public class Command : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}