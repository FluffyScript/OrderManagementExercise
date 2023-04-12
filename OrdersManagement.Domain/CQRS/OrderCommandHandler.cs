﻿using MediatR;
using OrdersManagement.Domain.Core.Interfaces;
using OrdersManagement.Domain.Core;
using OrdersManagement.Domain.Interfaces.Repositories;
using OrdersManagement.Domain.Core.Notification;
using OrdersManagement.Domain.CQRS.Commands;
using OrdersManagement.Domain.CQRS.Events;

namespace OrdersManagement.Domain.CQRS
{
    //- Create a new order
    //- Update the order delivery address
    //- Update the order items
    //- Cancel an order
    public class OrderCommandHandler : CommandHandler,
        IRequestHandler<CreateOrderCommand, bool>,
        IRequestHandler<UpdateOrderCommand, bool>,
        IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler Bus;

        public OrderCommandHandler(IOrderRepository orderRepository,
                                      IUnitOfWork uow,
                                      IMediatorHandler bus,
                                      INotificationHandler<Notification> notifications) : base(uow, bus, notifications)
        {
            _orderRepository = orderRepository;
            Bus = bus;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(request.Id);
            if (existingOrder != null)
            {
                await Bus.RaiseEvent(new Notification(request.MessageType, "Order already exists"));
                return false;
            }

            var order = new Order(request.Id, request.ProductName, request.DeliveryAddress);

            await _orderRepository.AddAsync(order);

            var result = await CommitAsync();
            if (result)
            {
                await Bus.RaiseEvent(new OrderCreated(order));
            }

            return true;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.Id, request.ProductName, request.DeliveryAddress);
            var existingOrder = await _orderRepository.GetById(order.Id);

            if (existingOrder != null && existingOrder.Id != order.Id)
            {
                if (!existingOrder.Equals(order))
                {
                    await Bus.RaiseEvent(new Notification(request.MessageType, "The order id already exists."));
                    return false;
                }
            }

            _orderRepository.Update(order);
            var result = await CommitAsync();

            if (result)
            {
                await Bus.RaiseEvent(new OrderUpdated(order));
            }

            return true;
        }

        public async Task<bool> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            await _orderRepository.RemoveAsync(request.Id);
            var result = await CommitAsync();

            if (result)
            {
                await Bus.RaiseEvent(new OrderCanceled(request.Id));
            }

            return true;
        }

        public void Dispose()
        {
            _orderRepository.Dispose();
        }
    }
}
