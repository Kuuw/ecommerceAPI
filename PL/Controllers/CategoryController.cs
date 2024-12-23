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
            var result = _categoryService.Get();
            return HandleServiceResult(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Category(int id)
        {
            var result = _categoryService.GetById(id);
            return HandleServiceResult(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Category(CategoryDTO categoryDTO)
        {
            var result = _categoryService.Add(categoryDTO);
            return HandleServiceResult(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateModel]
        public IActionResult Category(int id, CategoryDTO categoryDTO)
        {
            categoryDTO.CategoryId = id;
            var result = _categoryService.Update(categoryDTO);
            return HandleServiceResult(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult CategoryDelete(int id)
        {
            var result = _categoryService.Delete(id);
            return HandleServiceResult(result);
        }
    }
}
