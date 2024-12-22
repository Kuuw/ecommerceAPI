using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IAuthService
    {
        public ServiceResult<AuthenticateResponse?> Authenticate(UserLogin userLogin);
    }
}
