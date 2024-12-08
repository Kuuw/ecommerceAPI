using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class ProductDtoValidator : AbstractValidator<ProductDTO>
    {
        public ProductDtoValidator()
        {
            RuleFor(model => model.Name).NotEmpty().NotNull().WithMessage("Ürün adı boş geçilemez.");
            RuleFor(model => model.UnitPrice).NotEmpty().NotNull().WithMessage("Ürün fiyatı boş geçilemez.");
            RuleFor(model => model.CategoryId).NotEmpty().NotNull().WithMessage("Ürün kategorisi boş geçilemez.");
        }
    }
}
