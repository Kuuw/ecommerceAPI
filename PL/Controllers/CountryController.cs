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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService service)
        {
            _countryService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Country()
        {
            var countries = _countryService.Get();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Country(int id)
        {
            var country = _countryService.GetById(id);
            return Ok(country);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Country(CountryDTO countryDTO)
        {
            _countryService.Add(countryDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Country(int id, CountryDTO countryDTO)
        {
            countryDTO.CountryId = id;
            _countryService.Update(countryDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CountryDelete(int id)
        {
            _countryService.Delete(id);
            return Ok();
        }
    }
}
