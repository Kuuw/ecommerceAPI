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
            var items = _cartService.Get();
            return Ok(items);
        }

        [HttpPut]
        public IActionResult Cart(CartItemDTO cartItemDTO)
        {
            _cartService.Update(cartItemDTO);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public IActionResult Cart(int productId)
        {
            _cartService.Delete(productId);
            return Ok();
        }
    }
}
