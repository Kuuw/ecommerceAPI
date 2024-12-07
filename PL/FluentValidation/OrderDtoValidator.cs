using Entities.DTO;
using FluentValidation;

namespace PL.FluentValidation
{
    public class OrderDtoValidator : AbstractValidator<OrderDTO>
    {
        public OrderDtoValidator()
        {
            RuleFor(model => model.OrderItems).NotEmpty().NotNull().WithMessage("Siparişinizde en az 1 ürün bulunmalı.");
        }
    }
}
