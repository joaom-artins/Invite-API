using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class ResponsibleCreateRequestValidator : AbstractValidator<ResponsibleCreateRequest>
{
    public ResponsibleCreateRequestValidator()
    {
        RuleFor(x => x.Name)
         .NotEmpty().WithMessage("Nome é um campo obrigatório!")
         .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
         .MaximumLength(60).WithMessage("Nome deve ter no máximo 60 caractéres");

        RuleFor(x => x.CPF)
            .NotEmpty().WithMessage("CPF é um campo obrigatório!")
            .MaximumLength(14).WithMessage("CPF deve ter no máximo 14 caractéres");

        RuleFor(x => x.PersonInFamily)
            .NotEmpty().WithMessage("Pessoas na família é um campo obrigatório!");

        RuleForEach(x => x.Persons)
            .SetValidator(new PersonCreateRequestValidator())
            .When(x => x.Persons is not null);
    }
}
