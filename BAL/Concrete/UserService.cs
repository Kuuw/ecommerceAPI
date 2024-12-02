using AutoMapper;
using BAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class UserService : IUserService
    {
        UserRepository userRepository = new UserRepository();
        BcryptService bcryptService = new BcryptService();
        Mapper mapper = MapperConfig.InitializeAutomapper();


        public User Register(UserDTO userData)
        {
            var user = mapper.Map<User>(userData);

            user.Role = "User";
            user.UpdatedAt = DateTime.Now;
            user.CreatedAt = DateTime.Now;
            user.Addresses = [];
            user.CartItems = [];
            user.Orders = [];
            user.PasswordHash = bcryptService.HashPassword(user.PasswordHash);

            userRepository.Insert(user);
            return user;
        }

        public UserDTO? GetByEmail(string email)
        {
            User? user = userRepository.Where(x => x.Email == email).FirstOrDefault();

            return mapper.Map<UserDTO>(user);
        }

        public UserDTO? GetById(int userId)
        {
            User? user = userRepository.GetById(userId);
            
            return mapper.Map<UserDTO>(user);
        }
        public bool Update(UserDTO userData, int userId)
        {
            User? user = userRepository.GetById(userId);
            if (user == null) { return false; }
            mapper.Map(userData, user);

            userRepository.Update(user);
            return true;
        }
    }
}
