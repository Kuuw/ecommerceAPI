using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();
        private readonly DbSet<OrderItem> _orderItems;
        private readonly DbSet<Order> _order;

        public OrderRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _orderItems = context.Set<OrderItem>();
            _order = context.Set<Order>();
        }

        public void DeleteItem(OrderItem item)
        {
            _orderItems.Remove(item);
            _context.SaveChanges();
        }

        public List<Order> Get(int userId)
        {
            var orders = _order.Where(x => x.UserId == userId)
                               .Include(x => x.OrderItems)
                               .ToList();

            return orders;
        }

        public List<OrderItem> GetItems(int OrderId)
        {
            return _orderItems.Where(x => x.OrderId == OrderId).ToList();
        }

        public void InsertItem(OrderItem item)
        {
            _orderItems.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(OrderItem item)
        {
            _orderItems.Update(item);
            _context.SaveChanges();
        }
    }
}
