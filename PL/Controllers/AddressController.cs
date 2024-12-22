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
            var result = _addressService.GetByUserId();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult AddressById(int id)
        {
            var result = _addressService.GetByAddressId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(AddressDTO addressDTO)
        {
            var result = _addressService.Add(addressDTO);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            var result = _addressService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(int id, AddressDTO addressDTO)
        {
            var result = _addressService.Update(addressDTO);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }
    }
}
