namespace OrdersManagement.Domain.CQRS.Commands
{
    public class UpdateOrderCommand : OrderCommand
    {
        public UpdateOrderCommand(Guid id, string productName, string deliveryAddress)
        {
            Id = id;
            ProductName = productName;
            DeliveryAddress = deliveryAddress;
        }
    }
}
