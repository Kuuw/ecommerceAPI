using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IUserService
    {
        public ServiceResult<bool> Register(UserDTO userData);
        public ServiceResult<UserDTO?> GetByEmail(string email);
        public ServiceResult<UserDTO?> GetById();
        public ServiceResult<bool> Update(UserDTO userData);
    }
}
