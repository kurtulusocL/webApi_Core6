using WebApiCore6.DataAccess.Concrete.EntityFramework.Context;
using WebApiCore6.Entities;
using WebApiCore6.Interfaces;

namespace WebApiCore6.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Country entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(Country entity)
        {
            _context.Remove(entity);
            return Save();
        }

        public ICollection<Country> GetAll()
        {
            return _context.Countries.OrderByDescending(i => i.CreatedDate).ToList();
        }

        public ICollection<Company> GetAllCompanyByCountryId(int countryId)
        {
            return _context.Companies.Where(i => i.Country.Id==countryId).ToList();
        }

        public Country GetById(int id)
        {
            return _context.Countries.Where(i => i.Id == id).FirstOrDefault();
        }

        public Country GetCountryByCompanyId(int companyId)
        {
            return _context.Companies.Where(i => i.Id == companyId).Select(i => i.Country).FirstOrDefault();
        }

        public bool IsCountryExist(int id)
        {
            return _context.Countries.Any(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Country entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}
