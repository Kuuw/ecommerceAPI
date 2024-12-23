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
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService service)
        {
            _orderService = service;
        }

        [HttpGet]
        public IActionResult Order()
        {
            var result = _orderService.Get();
            return HandleServiceResult(result);
        }

        [HttpGet("{id}")]
        public IActionResult Order(int id)
        {
            var result = _orderService.GetById(id);
            return HandleServiceResult(result);
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Order(OrderDTO orderDTO)
        {
            var result = _orderService.Add(orderDTO);
            return HandleServiceResult(result);
        }
    }
}
