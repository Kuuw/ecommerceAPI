using Entities.Models;

namespace DAL.Abstract
{
    public interface ICartRepository : IGenericRepository<CartItem>
    {
        public List<CartItem> ListWithProductData(int userId);
    }
}
