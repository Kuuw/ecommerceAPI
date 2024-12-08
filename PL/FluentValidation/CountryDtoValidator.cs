using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class CountryDtoValidator : AbstractValidator<CountryDTO>
    {
        public CountryDtoValidator()
        {
            RuleFor(model => model.CountryName).NotEmpty().NotNull().WithMessage("Ülke adı boş geçilemez.");
            RuleFor(model => model.CountryPhoneCode).NotEmpty().NotNull().WithMessage("Ülke telefon kodu boş geçilemez.");
        }
    }
}
