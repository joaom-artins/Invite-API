using FluentValidation;
using Invite.Commons;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class HallupdateRequestValidator : AbstractValidator<HallUpdateRequest>
{
    public HallupdateRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage("Nome é um campo obrigatório!")
           .MinimumLength(4).WithMessage("Nome deve conter pelo menos 4 caracteres!")
           .MaximumLength(60).WithMessage("Nome pode conter no máximo 60 caracteres!");

        RuleFor(x => x.OwnerNumber)
            .NotEmpty().WithMessage("Número do proprietário é um campo obrigatório!")
            .MinimumLength(11).WithMessage("Número do proprietário deve conter pelo menos 4 caracteres!")
            .MaximumLength(14).WithMessage("Número do proprietário pode conter no máximo 60 caracteres!")
            .Matches(ValidateStrings.PhoneNumber).WithMessage("Número inválido!");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Cidade do evento é um campo obrigatório!")
            .MinimumLength(3).WithMessage("Cidade deve conter pelo menos 3 caracteres")
            .MaximumLength(40).WithMessage("Cidade deve conter no máximo 40 caracteres");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Rua do evento é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Rua deve conter pelo menos 3 caracteres")
            .MaximumLength(50).WithMessage("Rua deve conter no máximo 40 caracteres");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("Estado do evento é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Estado deve conter pelo menos 3 caracteres")
            .MaximumLength(30).WithMessage("Estado deve conter no máximo 40 caracteres");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Número é um campo obrigatório!")
            .MinimumLength(1).WithMessage("Número deve conter pelo menos 3 caracteres")
            .MaximumLength(5).WithMessage("Número deve conter no máximo 40 caracteres");

        RuleFor(x => x.CEP)
            .NotEmpty().WithMessage("CEP é um campo obrigatório!")
            .MinimumLength(8).WithMessage("CEP deve conter pelo menos 3 caracteres")
            .MaximumLength(9).WithMessage("CEP deve conter no máximo 40 caracteres");
    }
}
