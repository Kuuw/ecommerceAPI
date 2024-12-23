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
            var result = _countryService.Get();
            return HandleServiceResult(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Country(int id)
        {
            var result = _countryService.GetById(id);
            return HandleServiceResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Country(CountryDTO countryDTO)
        {
            var result = _countryService.Add(countryDTO);
            return HandleServiceResult(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Country(int id, CountryDTO countryDTO)
        {
            countryDTO.CountryId = id;
            var result = _countryService.Update(countryDTO);
            return HandleServiceResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CountryDelete(int id)
        {
            var result = _countryService.Delete(id);
            return HandleServiceResult(result);
        }
    }
}
