using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {

    }
}
