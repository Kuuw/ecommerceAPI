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
    public class CategoryController : BaseController
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
            return HandleServiceResult(_categoryService.Get());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Category(int id)
        {
            return HandleServiceResult(_categoryService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Category(CategoryDTO categoryDTO)
        {
            return HandleServiceResult(_categoryService.Add(categoryDTO));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Category(int id, CategoryDTO categoryDTO)
        {
            categoryDTO.CategoryId = id;
            return HandleServiceResult(_categoryService.Update(categoryDTO));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CategoryDelete(int id)
        {
            return HandleServiceResult(_categoryService.Delete(id));
        }
    }
}
