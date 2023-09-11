using AutoMapper;
using Sana.Store.Application.Commands.Orders;
using Sana.Store.Domain;
using Sana.Store.Entities.Dtos;
using Sana.Store.Entities.Models;

namespace Sana.Store.Application.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<CreateOrderDetailModel, OrderDetail>();
            CreateMap<CreateOrderCommandHandler.Command, Order>();
        }
    }
}
