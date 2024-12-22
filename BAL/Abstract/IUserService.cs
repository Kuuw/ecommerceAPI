using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IUserService
    {
        public User Register(UserDTO userData);
        public UserDTO? GetByEmail(string email);
        public UserDTO? GetById();
        public bool Update(UserDTO userData);
    }
}
