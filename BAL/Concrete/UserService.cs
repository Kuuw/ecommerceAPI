using BAL.Abstract;
using DAL.Concrete;
using Entities.Models;

namespace BAL.Concrete
{
    public class UserService : IUserService
    {
        UserRepository userRepository = new UserRepository();
        BcryptService bcryptService = new BcryptService();

        public User Register(User user)
        {
            user.IsAdmin = false;
            user.UpdatedAt = DateTime.Now;
            user.CreatedAt = DateTime.Now;
            user.Addresses = [];
            user.CartItems = [];
            user.Orders = [];
            var PasswordHash = bcryptService.HashPassword(user.PasswordHash);
            user.PasswordHash = PasswordHash;

            userRepository.Insert(user);
            return user;
        }

        public User? GetByEmail(string email)
        {
            User? user = userRepository.Where(x => x.Email == email).FirstOrDefault();

            return user;
        }

        public User? GetById(int userId)
        {
            User? user = userRepository.GetById(userId);

            return user;
        }
        public void Update(User user)
        {
            userRepository.Update(user);
        }
    }
}
