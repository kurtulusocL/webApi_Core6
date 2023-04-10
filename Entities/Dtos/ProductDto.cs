using WebApiCore6.Core.Entities;
using WebApiCore6.Core.Entities.EntityFramework;

namespace WebApiCore6.Entities.Dtos
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public int UnitInStock { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalIncome { get; set; }
    }
}
