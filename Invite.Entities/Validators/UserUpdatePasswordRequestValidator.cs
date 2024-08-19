using FluentValidation;
using Invite.Commons;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class UserUpdatePasswordRequestValidator : AbstractValidator<UserUpdatePasswordRequest>
{
    public UserUpdatePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Senha antiga é um campo obrigatório!");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Nova senha é um campo obrigatório!")
            .MinimumLength(6).WithMessage("Senha nova deve conter pelo menos 6 caracteres!")
            .Matches(ValidateStrings.Password).WithMessage("Senha deve ter pelo menos 1 caractere maiúsculo, 1 minúsculo e 1 especial");
    }
}
