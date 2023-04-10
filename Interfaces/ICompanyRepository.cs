using WebApiCore6.Entities;

namespace WebApiCore6.Interfaces
{
    public interface ICompanyRepository
    {
        ICollection<Company> GetAll();
        Company GetById(int id);
        ICollection<Company> GetAllCompanyByProductId(int productId);
        ICollection<Product> GEtProductByCompanyId(int companyId);
        bool IsCompanyExist(int id);
        bool Create(Company entity);
        bool Update(Company entity);
        bool Delete(Company entity);
        bool Save();
    }
}
