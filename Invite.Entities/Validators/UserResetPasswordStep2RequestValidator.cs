using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class UserResetPasswordStep2RequestValidator : AbstractValidator<UserResetPasswordStep2Request>
{
    public UserResetPasswordStep2RequestValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Código é um campo obrigatório!")
            .Length(6).WithMessage("Código deve ter 6 caracteres!");
    }
}
