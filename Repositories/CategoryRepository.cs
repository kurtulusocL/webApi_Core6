using WebApiCore6.DataAccess.Concrete.EntityFramework.Context;
using WebApiCore6.Entities;
using WebApiCore6.Interfaces;

namespace WebApiCore6.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(Category entity)
        {
            _context.Add(entity);
            return Save();
        }

        public bool Delete(Category entity)
        {
           _context.Remove(entity);
            return Save();
        }

        public ICollection<Category> GetAll()
        {
            return _context.Categories.OrderByDescending(i => i.CreatedDate).ToList();
        }

        public ICollection<Product> GetAllProductByCategoryId(int categoryId)
        {
            return _context.ProductCategories.Where(i => i.CategoryId == categoryId).Select(i => i.Product).ToList();
        }

        public Category GetById(int id)
        {
            return _context.Categories.Where(i => i.Id == id).FirstOrDefault();
        }

        public bool IsCategoryExist(int id)
        {
            return _context.Categories.Any(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Category entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}
