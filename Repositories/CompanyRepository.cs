using WebApiCore6.DataAccess.Concrete.EntityFramework.Context;
using WebApiCore6.Entities;
using WebApiCore6.Interfaces;

namespace WebApiCore6.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Company entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(Company entity)
        {
            _context.Remove(entity);
            return Save();
        }

        public ICollection<Company> GetAll()
        {
            return _context.Companies.OrderByDescending(i => i.CreatedDate).ToList();
        }

        public ICollection<Company> GetAllCompanyByProductId(int productId)
        {
            return _context.ProductCompanies.Where(i => i.Product.Id == productId).Select(i => i.Company).ToList();
        }

        public Company GetById(int id)
        {
            return _context.Companies.Where(i => i.Id == id).FirstOrDefault();
        }

        public ICollection<Product> GEtProductByCompanyId(int companyId)
        {
            return _context.ProductCompanies.Where(i => i.Company.Id == companyId).Select(i => i.Product).ToList();
        }

        public bool IsCompanyExist(int id)
        {
            return _context.Companies.Any(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Company entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}
