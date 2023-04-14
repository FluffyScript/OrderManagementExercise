using OrdersManagement.Domain.Core;

namespace OrdersManagement.Domain.CQRS.Commands
{
    public abstract class OrderCommand : Command
    {
        public Guid Id { get; protected set; }

        public string ProductName { get; protected set; }

        public string DeliveryAddress { get; protected set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
