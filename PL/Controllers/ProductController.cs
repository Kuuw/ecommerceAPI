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
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService service)
        {
            _productService = service;
        }

        [HttpPost("GetPaged")]
        [AllowAnonymous]
        public IActionResult Product(int page, int pageSize, [FromBody] ProductFilter? productFilter)
        {
            return HandleServiceResult(_productService.GetPaged(page, pageSize, productFilter));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Product(int id)
        {
            return HandleServiceResult(_productService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Product(ProductDTO productDTO)
        {
            return HandleServiceResult(_productService.Add(productDTO));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Product(int id, ProductDTO productDTO)
        {
            return HandleServiceResult(_productService.Update(productDTO, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return HandleServiceResult(_productService.Delete(id));
        }

        [HttpPut("Stock/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Stock(int id, int stock)
        {
            return HandleServiceResult(_productService.UpdateStock(id, stock));
        }

        [HttpPost("Image/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Image(int id, IFormFile file)
        {
            return HandleServiceResult(_productService.UploadImage(id, file));
        }

        [HttpDelete("Image/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Image(Guid id)
        {
            return HandleServiceResult(_productService.DeleteImage(id));
        }
    }
}
