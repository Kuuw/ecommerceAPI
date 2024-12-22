using Entities.DTO;

namespace BAL.Abstract
{
    public interface IOrderService
    {
        public List<OrderDTO> Get();
        public OrderDTO? GetById(int id);
        public void Delete(int id);
        public OrderDTO Add(OrderDTO orderDTO);
    }
}
