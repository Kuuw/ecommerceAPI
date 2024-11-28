using Asp.Versioning;
using AutoMapper;
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
        AuthService authService;
        Mapper mapper = MapperConfig.InitializeAutomapper();


        public UserController(IConfiguration config)
        {
            authService = new AuthService(config);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public User Register(UserRegister userData)
        {
            var newUser = mapper.Map<User>(userData);

            userService.Register(newUser);
            return newUser;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var token = authService.Authenticate(userLogin);
            if (token == null)
            {
                return NotFound();
            }

            var user = userService.GetUserFromEmail(userLogin.Email);
            var authenticateResponse = new AuthenticateResponse(user!, token);
            
            return Ok(authenticateResponse);
        }
    }
}
