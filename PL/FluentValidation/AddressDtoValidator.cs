using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class AddressDtoValidator : AbstractValidator<AddressDTO>
    {
        public AddressDtoValidator()
        {
            RuleFor(model => model.AddressLine1).NotEmpty().NotNull().WithMessage("Adres Satırı 1 boş geçilemez.");
            RuleFor(model => model.CountryId).NotEmpty().NotNull().WithMessage("Ülke boş geçilemez.");
            RuleFor(model => model.FirstName).NotEmpty().NotNull().WithMessage("İsim boş geçilemez.");
            RuleFor(model => model.LastName).NotEmpty().NotNull().WithMessage("Soyisim boş geçilemez.");
            RuleFor(model => model.PostalCode).NotEmpty().NotNull().WithMessage("Posta Kodu boş geçilemez.");
        }
    }
}
