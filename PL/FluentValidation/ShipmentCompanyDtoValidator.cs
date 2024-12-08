using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class ShipmentCompanyDtoValidator : AbstractValidator<ShipmentCompanyDTO>
    {
        public ShipmentCompanyDtoValidator()
        {
            RuleFor(model => model.CompanyName).NotEmpty().NotNull().WithMessage("Kargo şirketi adı boş geçilemez.");
        }
    }
}
