using System.Data;
using FluentValidation;
using Invite.Commons;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é um campo obrigatório!")
            .EmailAddress().WithMessage("Email inválido!");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Nome completo é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome completo deve conter no mínimo 5 caracteres!")
            .MaximumLength(60).WithMessage("Nome completo deve conter no máximo 60 caracteres");

        RuleFor(x => x.CPFOrCNPJ)
            .NotEmpty().WithMessage("CPF é um campo obrigatório!")
            .Must(value => ValidateCPF.IsValidCpf(value) && ValidateCPF.IsValidCpf(value)).WithMessage("CPF Inválido!");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é umcampo obrigatório!")
             .Matches(ValidateStrings.Password).WithMessage("Senha deve ter pelo menos 1 caractere maiúsculo, 1 minúsculo e 1 especial");
    }
}
