using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
    }
}
