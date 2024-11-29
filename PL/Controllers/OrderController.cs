using Asp.Versioning;
using AutoMapper;
using BAL.Concrete;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        Mapper mapper = MapperConfig.InitializeAutomapper();

        [HttpGet]
        [Authorize]
        public IActionResult Order()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Order(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Order(CountryDTO countryDTO)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Order(int id, CountryDTO countryDTO)
        {
            return Ok();
        }
    }
}
