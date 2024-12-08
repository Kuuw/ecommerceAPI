using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PL.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Iterate through all action arguments
            foreach (var argument in context.ActionArguments)
            {
                var model = argument.Value;
                if (model == null)
                {
                    continue;
                }

                var validatorType = typeof(IValidator<>).MakeGenericType(model.GetType());
                var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationResult = validator.Validate(new ValidationContext<object>(model));

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                        context.Result = new BadRequestObjectResult(new { Errors = errors });
                        return;
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
