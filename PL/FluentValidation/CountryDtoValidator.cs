using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class CountryDtoValidator : AbstractValidator<CountryDTO>
    {
        public CountryDtoValidator()
        {
        }
    }
}
