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
    public class AddressController : BaseController
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
            return HandleServiceResult(result);
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult AddressById(int id)
        {
            var result = _addressService.GetByAddressId(id);
            return HandleServiceResult(result);
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(AddressDTO addressDTO)
        {
            var result = _addressService.Add(addressDTO);
            return HandleServiceResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            var result = _addressService.Delete(id);
            return HandleServiceResult(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(int id, AddressDTO addressDTO)
        {
            var result = _addressService.Update(addressDTO);
            return HandleServiceResult(result);
        }
    }
}
