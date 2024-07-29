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

        RuleFor(x => x.HallId)
            .NotEmpty().WithMessage("Salão de festas é um campo obrigatório!")
            .When(x => x.UseHallRegistred);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Cidade do evento é um campo obrigatório!")
            .MinimumLength(3).WithMessage("Cidade deve conter pelo menos 3 caracteres")
            .MaximumLength(40).WithMessage("Cidade deve conter no máximo 40 caracteres")
            .When(x => x.UseHallRegistred!);

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Rua do evento é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Rua deve conter pelo menos 3 caracteres")
            .MaximumLength(50).WithMessage("Rua deve conter no máximo 40 caracteres")
            .When(x => x.UseHallRegistred!);

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("Estado do evento é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Estado deve conter pelo menos 3 caracteres")
            .MaximumLength(30).WithMessage("Estado deve conter no máximo 40 caracteres")
            .When(x => x.UseHallRegistred!);

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Número é um campo obrigatório!")
            .MinimumLength(1).WithMessage("Número deve conter pelo menos 3 caracteres")
            .MaximumLength(5).WithMessage("Número deve conter no máximo 40 caracteres")
            .When(x => x.UseHallRegistred!);

        RuleFor(x => x.CEP)
            .NotEmpty().WithMessage("CEP é um campo obrigatório!")
            .MinimumLength(8).WithMessage("CEP deve conter pelo menos 3 caracteres")
            .MaximumLength(9).WithMessage("CEP deve conter no máximo 40 caracteres")
            .When(x => x.UseHallRegistred!);
    }
}
