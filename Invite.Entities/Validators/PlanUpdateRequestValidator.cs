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

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Preço é um campo obrigatório!");

        RuleFor(x => x.MaxGuest)
           .NotEmpty().WithMessage("Número máximo de convidados é um campo obrigatório!");
    }
}
