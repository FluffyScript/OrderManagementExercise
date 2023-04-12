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
        private readonly IMediatorHandler Bus;

        public OrderService(IMapper mapper,
                                  IOrderRepository orderRepository,
                                  IMediatorHandler bus)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            Bus = bus;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAll()
        {
            var ordersSet = _orderRepository.GetAll();
            var mappedOrders = ordersSet.ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
            return mappedOrders;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAll(int skip, int take)
        {
            var results = _orderRepository.GetAll(new OrderPagination(skip, take))
                .ProjectTo<OrderViewModel>(_mapper.ConfigurationProvider);
            
            return results;
        }

        public async Task<OrderViewModel> GetById(Guid id)
        {
            var order = await _orderRepository.GetById(id);
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
