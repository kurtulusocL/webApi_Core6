using WebApiCore6.Entities;

namespace WebApiCore6.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
        Product GetById(int id);
        Product GetProductByName(string name);
        bool IsProductExist(int id);
        bool Create(int companyId, int categoryId, Product entity);
        bool Update(int companyId, int categoryId, Product entity);
        bool Delete(Product entity);
        bool Save();
    }
}
