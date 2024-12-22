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
            var addresses = _addressService.GetByUserId();

            return Ok(addresses);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult AddressById(int id)
        {
            var addresses = _addressService.GetByAddressId(id);

            return Ok(addresses);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(AddressDTO addressDTO)
        {
            var address = _addressService.Add(addressDTO);
            return Ok(address);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            var success = _addressService.Delete(id);

            if (success) { return Ok(); }
            else { return BadRequest(); }
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(int id, AddressDTO addressDTO)
        {
            var success = _addressService.Update(addressDTO);

            if (success) { return Ok(); }
            else { return BadRequest(); }
        }
    }
}
