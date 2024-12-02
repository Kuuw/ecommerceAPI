using Asp.Versioning;
using AutoMapper;
using BAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class AddressController : Controller
    {
        AddressService addressService = new AddressService();

        [HttpGet]
        [Authorize]
        public IActionResult Address()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var addresses = addressService.GetByUserId(userId);

            return Ok(addresses);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Address(AddressDTO addressDTO)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            addressDTO.UserId = userId;

            var address = addressService.Add(addressDTO);
            return Ok(address);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var success = addressService.Delete(id, userId);

            if (success) { return Ok(); }
            else { return BadRequest(); }
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Address(int addressId, AddressDTO addressDTO)
        {
            int userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var success = addressService.Update(addressDTO, addressId, userId);

            if (success) { return Ok(); } 
            else { return BadRequest(); }
        }
    }
}
