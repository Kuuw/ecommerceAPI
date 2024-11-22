using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class OrderRepository:GenericRepository<Order>, IOrderRepository
    {
    }
}
