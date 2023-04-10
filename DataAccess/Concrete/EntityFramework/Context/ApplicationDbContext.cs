using Microsoft.EntityFrameworkCore;
using WebApiCore6.Entities;

namespace WebApiCore6.DataAccess.Concrete.EntityFramework.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Server=KURTULUSOCAL\\KURTULUSOCL;Database=WebApi;user Id=sa;Password=123");
            base.OnConfiguring(optionBuilder);
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCompany> ProductCompanies { get; set; }
    }
}
