using AutoMapper;
using OrderManagementApplication.Models;
using OrdersManagement.Domain;
using OrdersManagement.Domain.CQRS.Commands;

namespace OrderManagementApplication.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderViewModel, Order>();

            CreateMap<OrderViewModel, CreateOrderCommand>()
                .ConstructUsing(c => new CreateOrderCommand(c.Id, c.ProductName, c.DeliveryAddress));
            CreateMap<OrderViewModel, UpdateOrderCommand>()
                .ConstructUsing(c => new UpdateOrderCommand(c.Id, c.ProductName, c.DeliveryAddress));
        }
    }
}
