using Asp.Versioning;
using BAL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.ActionFilters;
using System.Security.Claims;

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
        [ValidateModel]
        public IActionResult Register(UserDTO userData)
        {
            var user = _userService.Register(userData);
            return Ok(user);
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
            var user = _userService.GetById();

            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        public IActionResult UserPut(UserDTO userDTO)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)!.Value;
            var user = _userService.GetById();
            if (user == null)
            {
                return NotFound();
            }
            userDTO.Role = userRole;
            _userService.Update(userDTO);
            return Ok();
        }
    }
}
