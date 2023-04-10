using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int UnitInStock { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalIncome { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
        public ICollection<ProductCompany> ProductCompanies { get; set; }
        public Product()
        {
            TotalIncome = SalePrice - BuyingPrice;
        }
    }
}
