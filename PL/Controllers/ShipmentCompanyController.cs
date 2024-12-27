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
    public class ShipmentCompanyController : BaseController
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
            return HandleServiceResult(_shipmentCompanyService.Get());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult ShipmentCompany(int id)
        {
            return HandleServiceResult(_shipmentCompanyService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult ShipmentCompany(ShipmentCompanyDTO shipmentCompanyDTO)
        {
            return HandleServiceResult(_shipmentCompanyService.Add(shipmentCompanyDTO));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult ShipmentCompany(int id, ShipmentCompanyDTO shipmentCompanyDTO)
        {
            shipmentCompanyDTO.ShipmentCompanyId = id;
            return HandleServiceResult(_shipmentCompanyService.Update(shipmentCompanyDTO));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ShipmentCompanyDelete(int id)
        {
            return HandleServiceResult(_shipmentCompanyService.Delete(id));
        }
    }
}
