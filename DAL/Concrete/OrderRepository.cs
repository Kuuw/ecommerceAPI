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
        private readonly DbSet<ProductStock> _stock;

        public OrderRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _orderItems = context.Set<OrderItem>();
            _order = context.Set<Order>();
            _stock = context.Set<ProductStock>();
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

        public List<OrderItem> GetItems(int orderId)
        {
            return _orderItems.Where(x => x.OrderId == orderId).ToList();
        }

        public new void Insert(Order order)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _order.Add(order);

                    // Reduce stock count for each order item
                    foreach (var orderItem in order.OrderItems)
                    {
                        var stock = _stock.FirstOrDefault(s => s.ProductId == orderItem.ProductId);
                        if (stock != null)
                        {
                            stock.Stock -= orderItem.Quantity;
                            if (stock.Stock < 0)
                            {
                                throw new InvalidOperationException("Insufficient stock for product: " + stock.ProductId);
                            }
                            stock.UpdatedAt = DateTime.UtcNow;
                            _stock.Update(stock);
                        }
                    }

                    _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        public void UpdateItem(OrderItem item)
        {
            _orderItems.Update(item);
            _context.SaveChanges();
        }

        public new Order? GetById(int id)
        {
            return _order.Include(x => x.OrderItems)
                         .ThenInclude(x => x.Product)
                         .ThenInclude(x => x.ProductImages)
                         .Include(x => x.Address)
                         .FirstOrDefault(x => x.OrderId == id);
        }
    }
}
