using Entities.Models;

namespace BAL.Abstract
{
    public interface IUserService
    {
        public User Register(User user);
        public User? GetByEmail(string email);
        public User? GetById(int id);
        public void Update(User user);
    }
}
