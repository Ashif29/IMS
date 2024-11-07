using AutoMapper;
using IMS.Application.Products.Dtos;
using IMS.Domain.Products;
using IMS.Domain.Products.Dtos;

namespace IMS.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateUpdateDto, ProductCreateUpdateDomainDto>();
        }
    }
}
