using Asp.Versioning;
using AutoMapper;
using BAL.Concrete;
using Entities.DTO;
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
        Mapper mapper = MapperConfig.InitializeAutomapper();

        [HttpGet("List")]
        public void List(/* TODO */)
        {
            // TODO
            return;
        }

        [HttpPost]
        [HttpPost("Add")]
        public Product Product(ProductDTO productDTO)
        {
            var product = mapper.Map<Product>(productDTO);
            productService.AddProduct(product);
            return product;
        }
    }
}
