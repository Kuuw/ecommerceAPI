using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        EcommerceDbContext context = new EcommerceDbContext();
        DbSet<ProductStock> data;
        public void AddStockEntry(int ProductId, int Stock)
        {
            ProductStock ps = new ProductStock();
            ps.ProductId = ProductId;
            ps.Stock = Stock;
            ps.UpdatedAt = DateTime.Now;
            ps.CreatedAt = DateTime.Now;

            data.Add(ps);
            context.SaveChanges();
        }
    }
}
