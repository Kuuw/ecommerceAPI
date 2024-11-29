using Entities.Models;

namespace BAL.Abstract
{
    public interface IProductService
    {
        public Product Add(Product product);
        public Product? GetById(int id);
        public List<Product> GetPaged(int page, int pageSize);
        public void Delete(int id);
        public void Update(Product product);
    }
}
