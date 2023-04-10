using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Company> Companies { get; set; }
       
    }
}
