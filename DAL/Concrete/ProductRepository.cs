using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        EcommerceDbContext context = new EcommerceDbContext();
        DbSet<ProductStock> stockData;
        DbSet<Product> productData;
        public void AddStockEntry(int ProductId, int Stock)
        {
            ProductStock ps = new ProductStock();
            ps.ProductId = ProductId;
            ps.Stock = Stock;
            ps.UpdatedAt = DateTime.Now;
            ps.CreatedAt = DateTime.Now;

            stockData.Add(ps);
            context.SaveChanges();
        }

        public List<Product> GetPaged(int page, int pageSize)
        {
            var items = productData.OrderBy(data => data.ProductId)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            return items;
        }
    }
}
