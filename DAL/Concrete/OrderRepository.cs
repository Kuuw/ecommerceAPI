using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly EcommerceDbContext context = new EcommerceDbContext();
        private readonly DbSet<OrderItem> _orderItems;

        public void DeleteItem(OrderItem item)
        {
            _orderItems.Remove(item);
            context.SaveChanges();
        }

        public List<OrderItem> GetItems(int OrderId)
        {
            return _orderItems.Where(x => x.OrderId == OrderId).ToList();
        }

        public void InsertItem(OrderItem item)
        {
            _orderItems.Add(item);
            context.SaveChanges();
        }

        public void UpdateItem(OrderItem item)
        {
            _orderItems.Update(item);
            context.SaveChanges();
        }
    }
}
