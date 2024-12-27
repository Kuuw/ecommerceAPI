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
    public class CountryController : BaseController
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
            return HandleServiceResult(_countryService.Get());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Country(int id)
        {
            return HandleServiceResult(_countryService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Country(CountryDTO countryDTO)
        {
            return HandleServiceResult(_countryService.Add(countryDTO));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Country(int id, CountryDTO countryDTO)
        {
            countryDTO.CountryId = id;
            return HandleServiceResult(_countryService.Update(countryDTO));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CountryDelete(int id)
        {
            return HandleServiceResult(_countryService.Delete(id));
        }
    }
}
