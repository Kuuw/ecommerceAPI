using BAL.Concrete;
using DAL.Models;
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
    public class ProductController : ControllerBase
    {
        ProductRepository productRepository = new ProductRepository();


        [HttpGet("List")]
        public void List(bool OnlyShowInStock, int? UnitPriceLowerThan, int? UnitPriceBiggerThan, String? NameLike)
        {
            // TODO
            return;
        }
    }
}
