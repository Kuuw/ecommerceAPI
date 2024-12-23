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
    public class CartController : BaseController
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
            return HandleServiceResult(result);
        }

        [HttpPut]
        public IActionResult Cart(CartItemDTO cartItemDTO)
        {
            var result = _cartService.Update(cartItemDTO);
            return HandleServiceResult(result);
        }

        [HttpDelete("{productId}")]
        public IActionResult Cart(int productId)
        {
            var result = _cartService.Delete(productId);
            return HandleServiceResult(result);
        }
    }
}
