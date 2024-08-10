using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class LeadCreateRequestValidator : AbstractValidator<LeadCreateRequest>
{
    public LeadCreateRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(4).WithMessage("Nome deve ter no mínimo 4 caracteres!")
            .MaximumLength(60).WithMessage("Nome deve ter no máximo 60 caracteres!");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Email deve ter no mínimo 5 caracteres!")
            .MaximumLength(40).WithMessage("Email deve ter no máximo 60 caracteres!");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Número de telefone é um campo obrigatório!")
            .MinimumLength(11).WithMessage("Número de telefone deve ter no mínimo 11 caracteres!")
            .MaximumLength(15).WithMessage("Número de telefone deve ter no máximo 15 caracteres!");
    }
}
