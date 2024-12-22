using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using Entities.Context.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IBcryptService _bcryptService;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();
        private readonly IUserContext _userContext;

        public UserService(IBcryptService bcrypt, IUserRepository repository, IUserContext userContext)
        {
            _bcryptService = bcrypt;
            _userRepository = repository;
            _userContext = userContext;
        }

        public ServiceResult<bool> Register(UserDTO userData)
        {
            var user = mapper.Map<User>(userData);

            user.Role = "User";
            user.Addresses = [];
            user.CartItems = [];
            user.Orders = [];
            user.PasswordHash = _bcryptService.HashPassword(user.PasswordHash);

            _userRepository.Insert(user);
            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<UserDTO?> GetByEmail(string email)
        {
            User? user = _userRepository.Where(x => x.Email == email).FirstOrDefault();

            return ServiceResult<UserDTO?>.Ok(mapper.Map<UserDTO>(user));
        }

        public ServiceResult<UserDTO?> GetById()
        {
            User? user = _userRepository.GetById(_userContext.UserId);
            
            return ServiceResult<UserDTO?>.Ok(mapper.Map<UserDTO>(user));
        }
        public ServiceResult<bool> Update(UserDTO userData)
        {
            userData.UserId = _userContext.UserId;
            userData.Role = _userContext.Role;
            User? user = _userRepository.GetById((int)userData.UserId!);
            if (user == null) { return ServiceResult<bool>.NotFound("User not found."); }
            userData.Password = _bcryptService.HashPassword(userData.Password);
            mapper.Map(userData, user);

            _userRepository.Update(user);
            return ServiceResult<bool>.Ok(true);
        }
    }
}
