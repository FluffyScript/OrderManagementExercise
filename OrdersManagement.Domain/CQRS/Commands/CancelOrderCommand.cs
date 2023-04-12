namespace OrdersManagement.Domain.CQRS.Commands
{
    public class CancelOrderCommand : OrderCommand
    {
        public CancelOrderCommand(Guid orderId)
        {
            Id = orderId;
        }
    }
}
