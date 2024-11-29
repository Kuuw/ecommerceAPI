using Entities.DTO;

namespace BAL.Abstract
{
    public interface IAuthService
    {
        public string? Authenticate(UserLogin userLogin);
    }
}
