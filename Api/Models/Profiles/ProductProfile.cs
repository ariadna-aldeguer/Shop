using Api.Models.Dtos;
using AutoMapper;
using Data.Database.Entities;

namespace Api.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(s => s.Size, a => a.MapFrom(m => m.Size.Name))
                .ForMember(s => s.Color, a => a.MapFrom(m => m.Color.Name))
                .ReverseMap();

        }
    }
}
