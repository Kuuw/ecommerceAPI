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
            return HandleServiceResult(_orderService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Order(int id)
        {
            return HandleServiceResult(_orderService.GetById(id));
        }

        [HttpPost]
        [ValidateModel]
        public IActionResult Order(OrderDTO orderDTO)
        {
            return HandleServiceResult(_orderService.Add(orderDTO));
        }
    }
}
