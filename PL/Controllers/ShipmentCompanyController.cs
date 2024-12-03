using Asp.Versioning;
using BAL.Abstract;
using BAL.Concrete;
using DAL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ShipmentCompanyController : ControllerBase
    {
        IShipmentCompanyService _shipmentCompanyService;

        public ShipmentCompanyController(IShipmentCompanyService service)
        {
            _shipmentCompanyService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ShipmentCompany()
        {
            var shipmentCompanies = _shipmentCompanyService.Get();
            return Ok(shipmentCompanies);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult ShipmentCompany(int id)
        {
            var shipmentComapny = _shipmentCompanyService.GetById(id);
            return Ok(shipmentComapny);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult ShipmentCompany(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            _shipmentCompanyService.Add(shipmentCompanyDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShipmentCompany(int id, ShipmentCompanyDTO shipmentCompanyDTO)
        {
            _shipmentCompanyService.Update(shipmentCompanyDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShipmentCompanyDelete(int id)
        {
            _shipmentCompanyService.Delete(id);
            return Ok();
        }
    }
}
