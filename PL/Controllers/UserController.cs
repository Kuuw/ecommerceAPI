using Asp.Versioning;
using BAL.Services;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


        [HttpPost("Register")]
        public User Register(User user)
        {
            userService.Register(user);
            return user;
        }
    }
}
