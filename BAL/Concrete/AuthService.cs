using BAL.Abstract;
using DAL.Abstract;
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
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IBcryptService _bcryptService;

        public AuthService(IConfiguration config, IUserRepository repository, IBcryptService bcrypt)
        {
            _config = config;
            _userRepository = repository;
            _bcryptService = bcrypt;
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
                new Claim(ClaimTypes.Role, user.Role)
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
            var user = _userRepository.Where(x => x.Email == userLogin.Email.ToLower()).FirstOrDefault();
            if (user == null) { return null; }

            if (_bcryptService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return Generate(user);
            }
            return null;
        }
    }
}
