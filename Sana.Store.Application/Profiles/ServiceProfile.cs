using AutoMapper;
using Sana.Store.Domain;
using Sana.Store.Entities.Dtos;

namespace Sana.Store.Application.Profiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
