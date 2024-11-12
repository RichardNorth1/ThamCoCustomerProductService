using AutoMapper;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Dtos;

namespace ThamCoCustomerProductService
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ProductDto,Product >().ReverseMap();
            CreateMap<CompanyDto, Company>().ReverseMap();
            CreateMap<CompanyProductsDto, CompanyProducts>().ReverseMap();
        }

    }
}
