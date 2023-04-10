using WebApiCore6.DataAccess.Concrete.EntityFramework.Context;
using WebApiCore6.Entities;
using WebApiCore6.Interfaces;

namespace WebApiCore6.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Create(int companyId, int categoryId, Product entity)
        {
            var productCompany = _context.Companies.Where(i => i.Id == companyId).FirstOrDefault();
            var productCategory = _context.Categories.Where(i => i.Id == categoryId).FirstOrDefault();

            var company = new ProductCompany()
            {
                Company = productCompany,
                Product = entity
            };

            _context.Add(company);

            var category = new ProductCategory()
            {
                Category = productCategory,
                Product = entity
            };
            _context.Add(category);
            _context.Add(entity);
            return Save();
        }

        public bool Delete(Product entity)
        {
            _context.Remove(entity);
            return Save();
        }

        public ICollection<Product> GetAll()
        {
            return _context.Products.OrderByDescending(i => i.CreatedDate).ToList();
        }

        public Product GetById(int id)
        {
            return _context.Products.Where(i => i.Id == id).FirstOrDefault();
        }

        public Product GetProductByName(string name)
        {
            return _context.Products.Where(i => i.Name == name).FirstOrDefault();
        }

        public bool IsProductExist(int id)
        {
            return _context.Products.Any(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(int companyId, int categoryId, Product entity)
        {
            _context.Update(entity);
            return Save();
        }
    }
}
