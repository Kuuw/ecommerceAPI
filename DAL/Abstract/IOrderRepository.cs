using Entities.Models;

namespace DAL.Abstract
{
    public interface IOrderRepository: IGenericRepository<Order>
    {
        public void InsertItem(OrderItem item);
        public void UpdateItem(OrderItem item);
        public void DeleteItem(OrderItem item);
        public List<OrderItem> GetItems(int OrderId);
    }
}
