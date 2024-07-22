using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class ResponsibleUpdateRequestValidator : AbstractValidator<ResponsibleUpdateRequest>
{
    public ResponsibleUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Nome é um campo obrigatório!")
           .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
           .MaximumLength(60).WithMessage("Nome deve ter no máximo 60 caractéres");

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF é um campo obrigatório!")
            .MaximumLength(14).WithMessage("CPF deve ter no máximo 14 caractéres");
    }
}
