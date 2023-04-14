using MediatR;
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
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler Bus;

        public OrderCommandHandler(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IUnitOfWork uow,
            IMediatorHandler bus,
            INotificationHandler<Notification> notifications) : base(uow, bus, notifications)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            Bus = bus;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetById(request.Id);
            if (existingOrder != null)
            {
                await Bus.RaiseEvent(new Notification(request.MessageType, "An order with the same id already exists."));
                return false;
            }

            var order = new Order(request.Id, request.ProductName, request.DeliveryAddress);
            order.Products = request.Products.ToList();
            foreach (var product in order.Products)
            {
                product.OrderId = order.Id;
            }

            await _orderRepository.AddAsync(order);
            foreach (var product in order.Products)
            {
                await _productRepository.AddAsync(product);
            }
            var result = await CommitAsync();

            if (result)
            {
                order.Products = GetPrunedProducts(request.Products);
                await Bus.RaiseEvent(new OrderCreated(order));
            }

            return true;
        }

        public async Task<bool> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.Id, request.ProductName, request.DeliveryAddress);
            order.Products = request.Products.ToList();
            foreach (var product in order.Products)
            {
                product.OrderId = order.Id;
                product.Order = order;
            }

            var existingOrder = await _orderRepository.GetById(order.Id);
            if (existingOrder == null)
            {
                await Bus.RaiseEvent(new Notification(request.MessageType, "The order to update doesn't exist."));
                return false;
            }

            await HandleProducts(order, existingOrder);

            _orderRepository.Update(order);
            var result = await CommitAsync();

            order.Products = GetPrunedProducts(request.Products);
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

        private async Task HandleProducts(Order updated, Order existing)
        {
            var productsToAdd = new List<Product>();
            var existingProductsNotMatched = existing.Products.ToList();

            foreach (var updatedProduct in updated.Products)
            {
                var updatedEntity = await _productRepository.GetByIdAsync(updatedProduct.Id);
                if (updatedEntity == null)
                {
                    productsToAdd.Add(updatedProduct);
                }

                var matchedExistingProduct = existingProductsNotMatched.FirstOrDefault(ep => ep.Id == updatedProduct.Id);
                if (matchedExistingProduct != null)
                {
                    _productRepository.Update(updatedProduct);
                    existingProductsNotMatched.Remove(matchedExistingProduct);
                }
            }

            foreach (var toAdd in productsToAdd)
            {
                await _productRepository.AddAsync(toAdd);
            }
            foreach (var toRemove in existingProductsNotMatched)
            {
                await _productRepository.RemoveAsync(toRemove.Id);
            }
        }

        private ICollection<Product> GetPrunedProducts(IEnumerable<Product> products)
        {
            var pruned = new List<Product>();
            foreach (var product in products)
            {
                product.Order = null;
                pruned.Add(product);
            }
            return pruned;
        }

    }
}
