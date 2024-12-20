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
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService service)
        {
            _addressService = service;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Address()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var addresses = _addressService.GetByUserId(userId);

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult AddressById(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var addresses = _addressService.GetByAddressId(id, userId);

            return Ok(addresses);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(AddressDTO addressDTO)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            addressDTO.UserId = userId;

            var address = _addressService.Add(addressDTO);
            return Ok(address);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var success = _addressService.Delete(id, userId);

            if (success) { return Ok(); }
            else { return BadRequest(); }
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(int id, AddressDTO addressDTO)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value!);
            addressDTO.AddressId = id;
            addressDTO.UserId = userId;
            var success = _addressService.Update(addressDTO, userId);

            if (success) { return Ok(); }
            else { return BadRequest(); }
        }
    }
}
