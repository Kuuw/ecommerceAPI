using Entities.DTO;

namespace BAL.Abstract
{
    public interface IOrderService
    {
        public List<OrderDTO> Get(int userId);
        public OrderDTO? GetById(int id, int userId);
        public void Delete(int id);
        public OrderDTO Add(OrderDTO orderDTO, int userId);
    }
}
