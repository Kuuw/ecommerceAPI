using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class UserDtoValidator : AbstractValidator<UserDTO>
    {
        public UserDtoValidator()
        {
            RuleFor(model => model.FirstName).NotEmpty().WithMessage("İsim gereklidir.");
            RuleFor(model => model.LastName).NotEmpty().WithMessage("Soyisim gereklidir.");
            RuleFor(model => model.Email).NotEmpty().WithMessage("Email adresi gereklidir.");
            RuleFor(model => model.Email).EmailAddress().WithMessage("Email geçerli değil.");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Şifre gereklidir.");
            RuleFor(model => model.Password).MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");
            RuleFor(model => model.Password).MaximumLength(20).WithMessage("Şifre en fazla 20 karakter olmalıdır.");
            RuleFor(model => model.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,20}$").WithMessage("Şifre en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
        }
    }
}
