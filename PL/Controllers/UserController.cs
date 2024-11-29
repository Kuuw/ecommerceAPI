using Asp.Versioning;
using AutoMapper;
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
        UserService userService = new UserService();
        AuthService authService;
        Mapper mapper = MapperConfig.InitializeAutomapper();


        public UserController(IConfiguration config)
        {
            authService = new AuthService(config);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public User Register(UserDTO userData)
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

            var user = userService.GetByEmail(userLogin.Email);
            var authenticateResponse = new AuthenticateResponse(user!, token);

            return Ok(authenticateResponse);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserGet()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var user = userService.GetById(userId);

            UserDTO userDTO = new();
            mapper.Map(user, userDTO);
            userDTO.Password = "private";

            return Ok(userDTO);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UserPut(UserDTO userDTO)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var user = userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            mapper.Map(userDTO, user);
            userService.Update(user);
            return Ok();
        }
    }
}
