using Asp.Versioning;
using AutoMapper;
using BAL.Abstract;
using BAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public UserController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;

        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public User Register(UserDTO userData)
        {
            return _userService.Register(userData);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLogin userLogin)
        {
            var token = _authService.Authenticate(userLogin);
            if (token == null)
            {
                return NotFound();
            }

            var user = _userService.GetByEmail(userLogin.Email);
            var authenticateResponse = new AuthenticateResponse(user!, token);

            return Ok(authenticateResponse);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserGet()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var user = _userService.GetById(userId);
            
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UserPut(UserDTO userDTO)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            _userService.Update(userDTO, userId);
            return Ok();
        }
    }
}
