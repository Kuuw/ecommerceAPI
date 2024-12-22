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
    public class ShipmentCompanyController : ControllerBase
    {
        private readonly IShipmentCompanyService _shipmentCompanyService;

        public ShipmentCompanyController(IShipmentCompanyService service)
        {
            _shipmentCompanyService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ShipmentCompany()
        {
            var result = _shipmentCompanyService.Get();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult ShipmentCompany(int id)
        {
            var result = _shipmentCompanyService.GetById(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult ShipmentCompany(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            var result = _shipmentCompanyService.Add(shipmentCompanyDTO);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult ShipmentCompany(int id, ShipmentCompanyDTO shipmentCompanyDTO)
        {
            shipmentCompanyDTO.ShipmentCompanyId = id;
            var result = _shipmentCompanyService.Update(shipmentCompanyDTO);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShipmentCompanyDelete(int id)
        {
            var result = _shipmentCompanyService.Delete(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }
    }
}
