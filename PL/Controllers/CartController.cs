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
            return HandleServiceResult(_cartService.Get());
        }

        [HttpPut]
        public IActionResult Cart(CartItemDTO cartItemDTO)
        {
            return HandleServiceResult(_cartService.Update(cartItemDTO));
        }

        [HttpDelete("{productId}")]
        public IActionResult Cart(int productId)
        {
            return HandleServiceResult(_cartService.Delete(productId));
        }
    }
}
