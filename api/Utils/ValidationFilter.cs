using System.Collections;
using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;
using Invite.Commons.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Utils;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var validationErrors = new List<ValidationFailure>();

        foreach (var argument in context.ActionArguments.Values)
        {
            if (argument is null)
            {
                continue;
            }

            if (argument is IEnumerable enumerableArgument && !(argument is string))
            {
                var index = 0;
                bool isEmpty = true;
                foreach (var item in enumerableArgument)
                {
                    isEmpty = false;
                    if (item == null)
                    {
                        continue;
                    }

                    var validator = GetValidatorForArgument(context, item);
                    if (validator != null)
                    {
                        var validationResult = await validator.ValidateAsync(new ValidationContext<object>(item));
                        if (!validationResult.IsValid)
                        {
                            foreach (var error in validationResult.Errors)
                            {
                                error.PropertyName = $"{error.PropertyName}[{index}]";
                                validationErrors.Add(error);
                            }
                        }
                    }
                    index++;
                }

                if (isEmpty)
                {
                    validationErrors.Add(new ValidationFailure("", NotificationMessage.Common.RequestListRequired));
                }
            }
            else
            {
                var validator = GetValidatorForArgument(context, argument);
                if (validator != null)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument));
                    if (!validationResult.IsValid)
                    {
                        validationErrors.AddRange(validationResult.Errors);
                    }
                }
            }
        }

        if (validationErrors.Count != 0)
        {
            var path = context.HttpContext.Request.Path;
            var result = new ValidationProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Status = StatusCodes.Status400BadRequest,
                Title = NotificationTitle.BadRequest,
                Detail = NotificationMessage.Common.ValidationError,
                Instance = path,
                Extensions = { { "traceId", Activity.Current?.Id } }
            };

            foreach (var failure in validationErrors)
            {
                if (result.Errors.ContainsKey(failure.PropertyName))
                {
                    result.Errors[failure.PropertyName] = result.Errors[failure.PropertyName].Concat(new[] { failure.ErrorMessage }).ToArray();
                }
                else
                {
                    result.Errors.Add(failure.PropertyName, new[] { failure.ErrorMessage });
                }
            }

            context.Result = new BadRequestObjectResult(result);
            return;
        }
        await next();
    }

    private static IValidator? GetValidatorForArgument(ActionExecutingContext context, object argument)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
        return (IValidator?)context.HttpContext.RequestServices.GetService(validatorType);
    }
}
