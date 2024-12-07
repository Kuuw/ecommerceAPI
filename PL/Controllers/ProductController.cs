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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Product(int page, int pageSize)
        {
            var response = _productService.GetPaged(page, pageSize);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Product(int id)
        {
            ProductDTO? product = _productService.GetById(id);

            if (product != null) { return Ok(product); }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Product(ProductDTO productDTO)
        {
            var product = _productService.Add(productDTO);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Product(int id, ProductDTO productDTO)
        {
            var success = _productService.Update(productDTO, id);
            if (success) { return Ok(); } else { return BadRequest(); }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok();
        }

        [HttpPut("Stock/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Stock(int id, int stock)
        {
            var success = _productService.UpdateStock(id, stock);
            if (success) { return Ok(); }
            else { return BadRequest(); }
        }
    }
}
