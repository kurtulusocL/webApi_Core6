using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
