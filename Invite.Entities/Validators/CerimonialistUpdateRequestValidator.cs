using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class CerimonialistUpdateRequestValidator : AbstractValidator<CerimonialistUpdateRequest>
{
    public CerimonialistUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(4).WithMessage("Nome deve conter no mínimo 4 caracteres!")
            .MaximumLength(50).WithMessage("Nome deve conter no máximo 50 caracteres!");

        RuleFor(x => x.StartPrice)
            .NotEmpty().WithMessage("Preço mínimo é um campo obrigatório!");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Cidade é um campo obrigatório!")
            .MinimumLength(3).WithMessage("Cidade deve conter pelo menos 3 caracteres")
            .MaximumLength(40).WithMessage("Cidade deve conter no máximo 40 caracteres");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("Estado é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Estado deve conter pelo menos 3 caracteres")
            .MaximumLength(30).WithMessage("Estado deve conter no máximo 40 caracteres");
    }
}
