using Entities.Models;

namespace DAL.Abstract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        public void AddStockEntry(int ProductId, int Stock);
    }
}
