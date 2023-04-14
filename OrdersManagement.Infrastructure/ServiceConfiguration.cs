using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrdersManagement.Domain.Core.Events;
using OrdersManagement.Domain.Core;
using OrdersManagement.Domain.Core.Notification;
using OrdersManagement.Domain.Interfaces.Repositories;
using OrdersManagement.Infrastructure.Data.Repositories;
using OrdersManagement.Domain.Core.Interfaces;
using OrdersManagement.Infrastructure.Data;
using OrdersManagement.Domain.Core.Interfaces.Repositories;
using OrdersManagement.Domain.CQRS;
using OrdersManagement.Domain.CQRS.Commands;
using OrdersManagement.Domain.CQRS.Events.Handlers;
using OrdersManagement.Domain.CQRS.Events;
using OrderManagementApplication;

namespace OrdersManagement.Infrastructure
{
    public class ServiceConfiguration
    {

        public static void RegisterServices(IServiceCollection services)
        {
            // services.AddHttpContextAccessor();

            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddScoped<IOrderService, OrderService>();

            // events
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
            services.AddScoped<INotificationHandler<OrderCreated>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdated>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderCanceled>, OrderEventHandler>();
            // commands
            services.AddScoped<IRequestHandler<CreateOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderCommand, bool>, OrderCommandHandler>();

            // services.AddScoped<IHttpService, HttpService>();

            // Repositories
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
        }
    }
}
