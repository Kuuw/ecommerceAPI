using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class CartRepository:GenericRepository<CartItem>, ICartRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly DbSet<CartItem> cartData;

        public CartRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            this.cartData = context.Set<CartItem>();
        }

        public List<CartItem> ListWithProductData(int userId)
        {
            var cartItems = cartData.Include(x => x.Product)
                                    .Where(x => x.UserId == userId)
                                    .ToList();

            return cartItems;
        }
    }
}
