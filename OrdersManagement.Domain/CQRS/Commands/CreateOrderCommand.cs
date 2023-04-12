namespace OrdersManagement.Domain.CQRS.Commands
{
    public class CreateOrderCommand : OrderCommand
    {
        public CreateOrderCommand(Guid id, string productName, string deliveryAddress)
        {
            Id = id;
            ProductName = productName;
            DeliveryAddress = deliveryAddress;
        }
    }
}
