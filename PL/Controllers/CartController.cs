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
            var result = _cartService.Get();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpPut]
        public IActionResult Cart(CartItemDTO cartItemDTO)
        {
            var result = _cartService.Update(cartItemDTO);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }

        [HttpDelete("{productId}")]
        public IActionResult Cart(int productId)
        {
            var result = _cartService.Delete(productId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return StatusCode(result.StatusCode, result.ErrorMessage);
        }
    }
}
