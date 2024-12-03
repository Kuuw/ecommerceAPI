using Asp.Versioning;
using BAL.Abstract;
using BAL.Concrete;
using Entities.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Category()
        {
            var categories = _categoryService.Get();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Category(int id)
        {
            var category = _categoryService.GetById(id);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Category(CategoryDTO categoryDTO)
        {
            _categoryService.Add(categoryDTO);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Category(int id, CategoryDTO categoryDTO)
        {
            categoryDTO.CategoryId = id;
            _categoryService.Update(categoryDTO);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CategoryDelete(int id)
        {
            _categoryService.Delete(id);
            return Ok();
        }
    }
}
