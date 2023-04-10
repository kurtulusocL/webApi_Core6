using WebApiCore6.Entities.Dtos;
using WebApiCore6.Interfaces;
using WebApiCore6.Repositories;

namespace WebApiCore6.Business.DependencyResolver.DepencencyInjection
{
    public static class DependencyContainer
    {
        public static void ContainerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        }
    }
}
