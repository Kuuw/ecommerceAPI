using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class ProductDtoValidator : AbstractValidator<ProductDTO>
    {
        public ProductDtoValidator()
        {
        }
    }
}
