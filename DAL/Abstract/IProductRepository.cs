using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        public void AddStockEntry(int ProductId, int Stock);
        public void UpdateStock(int ProductId, int Stock);
        public List<Product>? GetPaged(int page, int pageSize);
        public ProductStock GetStock(int ProductId);
    }
}
