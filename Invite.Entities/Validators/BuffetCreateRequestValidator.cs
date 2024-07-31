using FluentValidation;
using Invite.Commons;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class BuffetCreateRequestValidator : AbstractValidator<BuffetCreateRequest>
{
    public BuffetCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é um campo obrigatório!")
            .MinimumLength(5).WithMessage("Nome deve ter no mínimo 5 caracteres!")
            .MaximumLength(50).WithMessage("Nome deve ter no máximo 60 caracteres");

        RuleFor(x => x.CNPJ)
            .NotEmpty().WithMessage("CNPJ é um campo obrigatório!")
            .MinimumLength(14).WithMessage("CNPJ deve ter no mínimo 5 caracteres!")
            .MaximumLength(18).WithMessage("CNPJ deve ter no máximo 60 caracteres")
            .Matches(ValidateStrings.CNPJ).WithMessage("CNPJ inválido!"); ;

        RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Número do buffet é um campo obrigatório!")
           .MinimumLength(11).WithMessage("Número do buffet deve conter pelo menos 4 caracteres!")
           .MaximumLength(15).WithMessage("Número do buffet pode conter no máximo 14 caracteres!")
           .Matches(ValidateStrings.PhoneNumber).WithMessage("Número inválido!");

        RuleFor(x => x.City)
           .NotEmpty().WithMessage("Cidade do evento é um campo obrigatório!")
           .MinimumLength(3).WithMessage("Cidade deve conter pelo menos 3 caracteres")
           .MaximumLength(40).WithMessage("Cidade deve conter no máximo 40 caracteres");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("Estado é um campo obrigatório!")
            .MinimumLength(4).WithMessage("Estado deve conter pelo menos 4 caracteres")
            .MaximumLength(30).WithMessage("Estado deve conter no máximo 30 caracteres");
    }
}
