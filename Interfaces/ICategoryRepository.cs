using WebApiCore6.Entities;

namespace WebApiCore6.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetAll();
        Category GetById(int id);
        ICollection<Product> GetAllProductByCategoryId(int categoryId);
        bool IsCategoryExist(int id);
        bool Create(Category entity);
        bool Update(Category entity);
        bool Delete(Category entity);
        bool Save();
    }
}
