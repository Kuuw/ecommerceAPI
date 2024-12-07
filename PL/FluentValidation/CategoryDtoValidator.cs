using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDtoValidator()
        {
        }
    }
}
