using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }

        public Country Country { get; set; }
        public ICollection<ProductCompany> ProductCompanies { get; set; }
    }
}
