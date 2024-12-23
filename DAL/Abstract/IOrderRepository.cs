using Entities.Models;

namespace DAL.Abstract
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        public void UpdateItem(OrderItem item);
        public void DeleteItem(OrderItem item);
        public List<OrderItem> GetItems(int orderId);
        public List<Order> Get(int userId);
        public new Order? GetById(int id);
    }
}
