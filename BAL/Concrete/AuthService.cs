using BAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BAL.Concrete
{
    public class AuthService : IAuthService
    {
        private IConfiguration _config;
        UserRepository userRepository = new UserRepository();
        BcryptService bcryptService = new BcryptService();

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("IsAdmin", user.IsAdmin.ToString())
            };

            var token = new JwtSecurityToken(
              _config["JwtSettings:Issuer"],
              _config["JwtSettings:Audience"],
              claims,
              expires: DateTime.UtcNow.AddHours(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? Authenticate(UserLogin userLogin)
        {
            var user = userRepository.Where(x => x.Email == userLogin.Email.ToLower()).FirstOrDefault();

            if (bcryptService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return Generate(user);
            }
            return null;
        }
    }
}
