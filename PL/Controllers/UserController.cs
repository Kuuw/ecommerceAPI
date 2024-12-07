using Asp.Versioning;
using AutoMapper;
using BAL.Abstract;
using BAL.Concrete;
using Entities.DTO;
using Entities.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
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
        private readonly IValidator<UserDTO> _validator;

        public UserController(IUserService userService, IAuthService authService, IValidator<UserDTO> validator)
        {
            _userService = userService;
            _authService = authService;
            _validator = validator;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(UserDTO userData)
        {
            var result = _validator.Validate(userData);
            if (!result.IsValid)
            {
                List<String> errors = new();
                foreach (var error in result.Errors) { errors.Add(error.ErrorMessage); }
                return BadRequest(errors);
            }

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
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var user = _userService.GetById(userId);
            
            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UserPut(UserDTO userDTO)
        {
            var result = _validator.Validate(userDTO);
            if (!result.IsValid) 
            {
                List<String> errors = new();
                foreach(var error in result.Errors) { errors.Add(error.ErrorMessage); }
                return BadRequest(errors);
            }

            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var userRole = User.FindFirst(ClaimTypes.Role)!.Value;
            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            userDTO.UserId = userId;
            userDTO.Role = userRole;
            _userService.Update(userDTO);
            return Ok();
        }
    }
}
