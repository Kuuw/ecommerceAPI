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
            var result = _productService.GetPaged(page, pageSize, productFilter);
            return HandleServiceResult(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Product(int id)
        {
            var result = _productService.GetById(id);
            return HandleServiceResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Product(ProductDTO productDTO)
        {
            var result = _productService.Add(productDTO);
            return HandleServiceResult(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Product(int id, ProductDTO productDTO)
        {
            var result = _productService.Update(productDTO, id);
            return HandleServiceResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            return HandleServiceResult(result);
        }

        [HttpPut("Stock/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Stock(int id, int stock)
        {
            var result = _productService.UpdateStock(id, stock);
            return HandleServiceResult(result);
        }

        [HttpPost("Image/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Image(int id, IFormFile file)
        {
            var result = _productService.UploadImage(id, file);
            return HandleServiceResult(result);
        }

        [HttpDelete("Image/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Image(Guid id)
        {
            var result = _productService.DeleteImage(id);
            return HandleServiceResult(result);
        }
    }
}
