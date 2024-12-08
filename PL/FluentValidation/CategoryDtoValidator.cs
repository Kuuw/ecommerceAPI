using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class CategoryDtoValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDtoValidator()
        {
            RuleFor(model => model.Name).NotEmpty().NotNull().WithMessage("Kategori adı boş geçilemez.");
        }
    }
}
