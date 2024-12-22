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
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public IActionResult Order()
        {
            var orders = _orderService.Get();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult Order(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) { return BadRequest(); }
            return Ok(order);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Order(OrderDTO orderDTO)
        {
            _orderService.Add(orderDTO);
            return Ok();
        }
    }
}
