using DAL.Concrete;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AuthService
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
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var role = "";
            if (user.IsAdmin) { role = "Admin"; } else { role = "User"; };

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? Authenticate(UserLogin userLogin)
        {
            var user = userRepository.Where(x => x.Email == userLogin.Email.ToLower()).FirstOrDefault();
            
            if(bcryptService.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                return Generate(user);
            }
            return null;
        }
    }
}
