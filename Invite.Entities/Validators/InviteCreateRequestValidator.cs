using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class InviteCreateRequestValidator : AbstractValidator<InviteCreateRequest>
{
    public InviteCreateRequestValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Mensagem é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Mensagem deve ter no mínimo 5 caracteres!")
            .MaximumLength(120).WithMessage("Mensagem deve ter no máximo 120 caracteres!");

        RuleFor(x => x.LimitDate)
            .NotEmpty().WithMessage("Data límite de aceite é um campo obrigatório!");
    }
}