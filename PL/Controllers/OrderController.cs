using Asp.Versioning;
using AutoMapper;
using BAL.Abstract;
using BAL.Concrete;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var orders = _orderService.Get(userId);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult Order(int id)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var order = _orderService.GetById(id, userId);
            if (order == null) { return BadRequest(); }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Order(OrderDTO orderDTO)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            _orderService.Add(orderDTO, userId);
            return Ok();
        }
    }
}
