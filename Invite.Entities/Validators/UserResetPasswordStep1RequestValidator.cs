using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class UserResetPasswordStep1RequestValidator : AbstractValidator<UserResetPasswordStep1Request>
{
    public UserResetPasswordStep1RequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é um campo obrigatório!")
            .EmailAddress().WithMessage("Email inválido!");
    }
}
