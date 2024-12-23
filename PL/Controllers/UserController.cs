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
            var result = _userService.Register(userData);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Login(UserLogin userLogin)
        {
            var result = _authService.Authenticate(userLogin);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserGet()
        {
            var result = _userService.GetById();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPut]
        [Authorize]
        [ValidateModel]
        public IActionResult UserPut(UserDTO userDTO)
        {
            var result = _userService.Update(userDTO);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }
    }
}
