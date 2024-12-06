using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DAL.Concrete
{
    public class ProductRepository:GenericRepository<Product>, IProductRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly DbSet<ProductStock> stockData;
        private readonly DbSet<Product> productData;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.stockData = context.Set<ProductStock>();
            this.productData = context.Set<Product>();
        }

        public void AddStockEntry(int ProductId, int Stock)
        {
            ProductStock ps = new ProductStock();
            ps.ProductId = ProductId;
            ps.Stock = Stock;
            ps.UpdatedAt = DateTime.UtcNow;
            ps.CreatedAt = DateTime.UtcNow;

            stockData.Add(ps);
            _context.SaveChanges();
        }

        public void UpdateStock(int ProductId, int Stock)
        {
            ProductStock? ps = stockData.FirstOrDefault(x => x.ProductId == ProductId);
            if (ps == null)
            {
                AddStockEntry(ProductId, Stock);
            }
            else
            {
                ps.Stock = Stock;
                ps.UpdatedAt = DateTime.UtcNow;
                _context.SaveChanges();
            }
        }

        public List<Product>? GetPaged(int page, int pageSize)
        {
            var items = productData.OrderBy(data => data.ProductId)
                                   .Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .Include(e => e.ProductStock)
                                   .ToList();

            return items;
        }

        public ProductStock GetStock(int ProductId) 
        { 
            var stock = stockData.FirstOrDefault(x => x.ProductId == ProductId);
            if (stock == null)
            {
                AddStockEntry(ProductId, 0);

                var addedStock = stockData.FirstOrDefault(x => x.ProductId == ProductId);
                return addedStock!;
            }
            return stock;
        }

        public new void Insert(Product product)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    productData.Add(product);
                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }
    }
}
