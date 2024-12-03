using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBcryptService _bcryptService;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public UserService(IBcryptService bcrypt, IUserRepository repository)
        {
            _bcryptService = bcrypt;
            _userRepository = repository;
        }

        public User Register(UserDTO userData)
        {
            var user = mapper.Map<User>(userData);

            user.Role = "User";
            user.UpdatedAt = DateTime.Now;
            user.CreatedAt = DateTime.Now;
            user.Addresses = [];
            user.CartItems = [];
            user.Orders = [];
            user.PasswordHash = _bcryptService.HashPassword(user.PasswordHash);

            _userRepository.Insert(user);
            return user;
        }

        public UserDTO? GetByEmail(string email)
        {
            User? user = _userRepository.Where(x => x.Email == email).FirstOrDefault();

            return mapper.Map<UserDTO>(user);
        }

        public UserDTO? GetById(int userId)
        {
            User? user = _userRepository.GetById(userId);
            
            return mapper.Map<UserDTO>(user);
        }
        public bool Update(UserDTO userData, int userId)
        {
            User? user = _userRepository.GetById(userId);
            if (user == null) { return false; }
            mapper.Map(userData, user);

            _userRepository.Update(user);
            return true;
        }
    }
}
