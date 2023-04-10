using AutoMapper;
using WebApiCore6.Entities;
using WebApiCore6.Entities.Dtos;

namespace WebApiCore6.Business.DependencyResolver.Mapping.AutoMapper.Mappers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Company,CompanyDto>();
            CreateMap<CompanyDto, Company>();
        }
    }
}
