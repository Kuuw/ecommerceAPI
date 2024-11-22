using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class CartRepository:GenericRepository<CartItem>, ICartRepository
    {
    }
}
