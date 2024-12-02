using Entities.Models;

namespace Entities.DTO
{
    public class AuthenticateResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserDTO user, string token)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Token = token;
        }
    }
}
