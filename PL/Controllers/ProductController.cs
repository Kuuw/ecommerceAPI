using Asp.Versioning;
using BAL.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : ControllerBase
    {
        ProductService productService = new ProductService();

        [HttpGet("List")]
        public void List(bool OnlyShowInStock, int? UnitPriceLowerThan, int? UnitPriceBiggerThan, String? NameLike)
        {
            // TODO
            return;
        }

        [Route]
        [HttpPost]
        [HttpPost("AddProduct")]
        public Product Product(Product product)
        {
            productService.AddProduct(product);
            return product;
        }

        [HttpPut("AddProduct")]
        public Product Product(Product product)
        {
            productService.AddProduct(product);
            return product;
        }

        [HttpDelete("AddProduct")]
        public Product Product(Product product)
        {
            productService.AddProduct(product);
            return product;
        }
    }
}
