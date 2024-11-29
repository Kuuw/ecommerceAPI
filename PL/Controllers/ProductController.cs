using Asp.Versioning;
using AutoMapper;
using BAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        ProductService productService = new ProductService();
        Mapper mapper = MapperConfig.InitializeAutomapper();

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Product(int page, int pageSize)
        {
            List<Product> products = productService.GetPaged(page, pageSize);
            int totalItems = productService.GetTotalCount();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var output = new {
                items = products,
                metadata = new
                {
                    page = page,
                    pageSize = pageSize,
                    totalItems = totalItems,
                    totalPages = totalPages
                }
            };

            return Ok(output);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Product(int id)
        {
            Product? product = productService.GetById(id);

            if (product != null)
            {
                return Ok(mapper.Map<ProductDTO>(product));
            }
            return NotFound();

        }

        [HttpPost]
        [Authorize(Roles="Admin")]
        public IActionResult Product(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            productService.Add(product);
            return Ok(product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Product(int id, ProductDTO productDTO)
        {
            Product? product = productService.GetById(id);

            if (product == null) { return NotFound(); }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.UnitPrice = productDTO.UnitPrice;
            product.UpdatedAt = DateTime.Now;

            productService.Update(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            productService.Delete(id);
            return Ok();
        }
    }
}
