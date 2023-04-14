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
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>()
                .ForMember(productVM => productVM.OrderId, opt => opt.Ignore())
                .ForMember(productVM => productVM.Order, opt => opt.Ignore());

            // Commands
            CreateMap<OrderViewModel, CreateOrderCommand>()
                .ForMember(command => command.Products, options => options.MapFrom(source => source.Products))
                .ConstructUsing(c => new CreateOrderCommand(c.Id, c.Name, c.DeliveryAddress));
            CreateMap<OrderViewModel, UpdateOrderCommand>()
                .ForMember(command => command.Products, options => options.MapFrom(source => source.Products))
                .ConstructUsing(c => new UpdateOrderCommand(c.Id, c.Name, c.DeliveryAddress));
        }
    }
}
