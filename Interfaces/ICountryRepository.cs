using WebApiCore6.Entities;

namespace WebApiCore6.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetAll();
        Country GetById(int id);
        Country GetCountryByCompanyId(int companyId);
        ICollection<Company> GetAllCompanyByCountryId(int countryId);
        bool IsCountryExist(int id);
        bool Create(Country entity);
        bool Update(Country entity);
        bool Delete(Country entity);
        bool Save();
    }
}
