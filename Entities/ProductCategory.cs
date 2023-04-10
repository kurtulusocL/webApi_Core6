using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities
{
    public class ProductCategory : BaseEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public Product Product{ get; set; }
        public Category Category { get; set; }
    }
}
