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
        Mapper mapper = MapperConfig.InitializeAutomapper();

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
            Address address = mapper.Map<Address>(addressDTO);
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);

            address.UserId = userId;
            addressService.Add(address);
            return Ok(address);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Address(int id)
        {
            Address? address = addressService.GetByAddressId(id);
            int userId = int.Parse(User.FindFirst("UserId")?.Value!);
            if (address == null || address.UserId == userId)
            {
                return NotFound();
            }
            addressService.Delete(address);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Address(int id, AddressDTO addressDTO)
        {
            Address? existingAddress = addressService.GetByAddressId(id);
            int userId = int.Parse(User.FindFirst("UserId")?.Value!);

            if (existingAddress == null || existingAddress.UserId != userId)
            {
                return NotFound();
            }

            mapper.Map(addressDTO, existingAddress);

            addressService.Update(existingAddress);

            return Ok(existingAddress);
        }
    }
}
