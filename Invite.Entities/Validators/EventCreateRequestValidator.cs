using FluentValidation;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class EventCreateRequestValidator : AbstractValidator<EventCreateRequest>
{
    public EventCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter no mínimo 5 caracteres!")
            .MaximumLength(60).WithMessage("Nome deve ter no máximo 60 caracteres");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Tipo de evento é um campo obrigatório!")
            .IsInEnum().WithMessage("Tipo inválido!");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Data do evento é um campo obrigatório");

        RuleFor(x => x.Guests)
            .NotEmpty().WithMessage("Número de convidados do evento é um campo obrigatório");
    }
}
