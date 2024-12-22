using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IOrderService
    {
        public ServiceResult<List<OrderDTO>> Get();
        public ServiceResult<OrderDTO?> GetById(int id);
        public ServiceResult<bool> Delete(int id);
        public ServiceResult<OrderDTO> Add(OrderDTO orderDTO);
    }
}
