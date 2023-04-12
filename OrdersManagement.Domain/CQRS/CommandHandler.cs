using MediatR;
using OrdersManagement.Domain.Core;
using OrdersManagement.Domain.Core.Interfaces;
using OrdersManagement.Domain.Core.Notification;

namespace OrdersManagement.Domain.CQRS
{
    public class CommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly NotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<Notification> notifications)
        {
            _uow = uow;
            _notifications = (NotificationHandler)notifications;
            _bus = bus;
        }

        public async Task<bool> CommitAsync()
        {
            if (_notifications.HasNotifications())
            {
                return false;
            }

            if (await _uow.CommitAsync())
            {
                return true;
            }

            await _bus.RaiseEvent(new Notification("Commit", "Could not commit changes"));
            return false;
        }
    }
}
