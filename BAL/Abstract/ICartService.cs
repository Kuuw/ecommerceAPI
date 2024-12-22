using Entities.DTO;

namespace BAL.Abstract
{
    public interface ICartService
    {
        public CartDTO Get();
        public void Update(CartItemDTO cartItemDTO);
        public void Delete(int productId);
    }
}
