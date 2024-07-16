using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class PlanUpdateRequestValidator : AbstractValidator<PlanUpdateRequest>
{
    public PlanUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Plano é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Plano deve ter no mínimo 5 caracteres!")
            .MaximumLength(40).WithMessage("Plano deve ter no máximo 40 caracteres");
    }
}
