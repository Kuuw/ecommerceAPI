using Asp.Versioning;
using BAL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.ActionFilters;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class UserController : BaseController
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
            return HandleServiceResult(_userService.Register(userData));
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLogin userLogin)
        {
            return HandleServiceResult(_authService.Authenticate(userLogin));
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserGet()
        {
            return HandleServiceResult(_userService.GetById());
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        public IActionResult UserPut(UserDTO userDTO)
        {
            return HandleServiceResult(_userService.Update(userDTO));
        }
    }
}
