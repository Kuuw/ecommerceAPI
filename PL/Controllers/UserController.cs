using Asp.Versioning;
using BAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        UserService userService = new UserService();
        BcryptService bcryptService = new BcryptService();
        AuthService authService;

        public UserController(IConfiguration config)
        {
            authService = new AuthService(config);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public User Register(UserRegister userData)
        {
            User newUser = new User();
            newUser.FirstName = userData.FirstName;
            newUser.LastName = userData.LastName;
            newUser.Email = userData.Email;
            newUser.Email = userData.Email;

            userService.Register(newUser);
            return newUser;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var token = authService.Authenticate(userLogin);

            if (token != null)
            {
                return Ok(token);
            }

            return NotFound();
        }
    }
}
