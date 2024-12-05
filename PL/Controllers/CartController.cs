using Asp.Versioning;
using BAL.Abstract;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService service)
        {
            _cartService = service;
        }

        [HttpGet]
        public IActionResult Cart()
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            var items = _cartService.Get(userId);
            return Ok(items);
        }

        [HttpPut]
        public IActionResult Cart(CartItemDTO cartItemDTO)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            _cartService.Update(userId, cartItemDTO);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public IActionResult Cart(int productId)
        {
            var userId = int.Parse(User.FindFirst("UserId")?.Value!);
            _cartService.Delete(userId, productId);
            return Ok();
        }
    }
}
