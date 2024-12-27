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
            return HandleServiceResult(_addressService.GetByUserId());
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult AddressById(int id)
        {
            return HandleServiceResult(_addressService.GetByAddressId(id));
        }

        [HttpPost]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(AddressDTO addressDTO)
        {
            return HandleServiceResult(_addressService.Add(addressDTO));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            return HandleServiceResult(_addressService.Delete(id));
        }

        [HttpPut("{id}")]
        [Authorize]
        [ValidateModel]
        public IActionResult Address(int id, AddressDTO addressDTO)
        {
            return HandleServiceResult(_addressService.Update(addressDTO));
        }
    }
}
