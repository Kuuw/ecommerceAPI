using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface ICartService
    {
        public ServiceResult<CartDTO> Get();
        public ServiceResult<bool> Update(CartItemDTO cartItemDTO);
        public ServiceResult<bool> Delete(int productId);
    }
}
