using Entities.DTO;

namespace BAL.Abstract
{
    public interface ICartService
    {
        public CartDTO Get(int userId);
        public void Update(int userId, CartItemDTO cartItemDTO);
        public void Delete(int userId, int productId);
    }
}
