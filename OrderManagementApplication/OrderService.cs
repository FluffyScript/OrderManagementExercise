using AutoMapper;
using AutoMapper.QueryableExtensions;
using OrderManagementApplication.Models;
using OrdersManagement.Domain;
using OrdersManagement.Domain.Core;
using OrdersManagement.Domain.CQRS.Commands;
using OrdersManagement.Domain.Interfaces.Repositories;

namespace OrderManagementApplication
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler Bus;

        public OrderService(IMapper mapper,
                            IOrderRepository orderRepository,
                            IProductRepository productRepository,
                            IMediatorHandler bus)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            Bus = bus;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAll()
        {
            var ordersSet = _orderRepository.GetAll().ToList();

            foreach (var order in ordersSet)
            {
                order.Products = _productRepository.GetProductsByOrder(order.Id).ToList();
            }

            var mappedOrders = _mapper.Map<IEnumerable<OrderViewModel>>(ordersSet);
            return mappedOrders;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAll(int skip, int take)
        {
            var ordersSet = _orderRepository.GetAll(new OrderPagination(skip, take));

            foreach(var order in ordersSet)
            {
                order.Products = _productRepository.GetProductsByOrder(order.Id).ToList();
            }

            var mappedResults = _mapper.Map<IEnumerable<OrderViewModel>>(ordersSet);
            return mappedResults;
        }

        public async Task<OrderViewModel> GetById(Guid id)
        {
            var order = await _orderRepository.GetById(id);

            if (order != null)
            {
                order.Products = _productRepository.GetProductsByOrder(id).ToList();
            }

            return _mapper.Map<OrderViewModel>(order);
        }

        public async Task Create(OrderViewModel orderViewModel)
        {
            var registerCommand = _mapper.Map<CreateOrderCommand>(orderViewModel);
            await Bus.SendCommand(registerCommand);
        }

        public async Task Update(OrderViewModel orderViewModel)
        {
            var updateCommand = _mapper.Map<UpdateOrderCommand>(orderViewModel);
            await Bus.SendCommand(updateCommand);
        }

        public async Task Cancel(Guid id)
        {
            try
            {
                var removeCommand = new CancelOrderCommand(id);
                await Bus.SendCommand(removeCommand);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
