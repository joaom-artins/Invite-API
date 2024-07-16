using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class PlanCreateRequestValidator : AbstractValidator<PlanCreateRequest>
{
    public PlanCreateRequestValidator()
    {
        RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Plano é um campo obrigatório!")
             .MinimumLength(5).WithMessage("Plano deve ter no mínimo 5 caracteres!")
             .MaximumLength(40).WithMessage("Plano deve ter no máximo 60 caracteres");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Tipo de plano é um campo obrigatório!")
            .IsInEnum().WithMessage("Tipo inválido!");
    }
}
