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
        private readonly DbSet<ProductImage> imageData;

        public ProductRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.stockData = context.Set<ProductStock>();
            this.productData = context.Set<Product>();
            this.imageData = context.Set<ProductImage>();
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

        public List<Product> GetPaged(int page, int pageSize, ProductFilter productFilter)
        {
            var query = productData.AsQueryable();

            switch (productFilter)
            {
                case { CategoryId: not null }:
                    query = query.Where(p => p.CategoryId == productFilter.CategoryId.Value);
                    break;
                case { MinPrice: not null }:
                    query = query.Where(p => p.UnitPrice >= productFilter.MinPrice.Value);
                    break;
                case { MaxPrice: not null }:
                    query = query.Where(p => p.UnitPrice <= productFilter.MaxPrice.Value);
                    break;
                case { ProductName: not null }:
                    query = query.Where(p => p.Name.Contains(productFilter.ProductName));
                    break;
            }

            var items = query.OrderBy(data => data.ProductId)
                             .Skip((page - 1) * pageSize)
                             .Take(pageSize)
                             .Include(e => e.ProductStock)
                             .Include(e => e.ProductImages)
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

        public int GetFilteredCount(ProductFilter productFilter)
        {
            var query = productData.AsQueryable();

            if (productFilter.CategoryId != null)
            {
                query = query.Where(p => p.CategoryId == productFilter.CategoryId.Value);
            }
            if (productFilter.MinPrice != null)
            {
                query = query.Where(p => p.UnitPrice >= productFilter.MinPrice.Value);
            }
            if (productFilter.MaxPrice != null)
            {
                query = query.Where(p => p.UnitPrice <= productFilter.MaxPrice.Value);
            }
            if (productFilter.ProductName != null)
            {
                query = query.AsEnumerable().Where(p => p.Name.Contains(productFilter.ProductName, StringComparison.OrdinalIgnoreCase)).AsQueryable();
            }

            return query.Count();
        }

        public void AddImage(ProductImage productImage)
        {
            imageData.Add(productImage);
            _context.SaveChanges();
        }

        public List<ProductImage> GetImages(int productId)
        {
            return imageData.Where(x => x.ProductId == productId).ToList();
        }

        public void DeleteImage(Guid imageId)
        {
            ProductImage? image = imageData.FirstOrDefault(x => x.ProductImageId == imageId);
            if (image == null)
            { throw new Exception("Image not found"); }
            imageData.Remove(image);
            _context.SaveChanges();
        }

        public new Product? GetById(int id)
        {
            var product = productData.Include(x => x.ProductImages).FirstOrDefault(x => x.ProductId == id);
            return product;
        }
    }
}
