using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities
{
    public class ProductCompany : BaseEntity
    {
        public int ProductId { get; set; }
        public int CompanyId { get; set; }

        public Product Product { get; set; }
        public Company Company  { get; set; }
    }
}
