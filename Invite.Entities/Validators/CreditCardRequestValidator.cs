using FluentValidation;
using Invite.Commons;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class CreditCardRequestValidator : AbstractValidator<CreditCardRequest>
{
    public CreditCardRequestValidator()
    {
        RuleFor(x => x.HolderName)
                .NotEmpty().WithMessage("Nome do titular do cartão é obrigatório");

        RuleFor(x => x.HolderCpfCnpj)
            .NotEmpty().WithMessage("CPF ou CNPJ do titular do cartão é obrigatório");

        RuleFor(x => x.HolderEmail)
            .NotEmpty().WithMessage("E-mail do titular do cartão é obrigatório");

        RuleFor(x => x.HolderMobilePhone)
            .NotEmpty().WithMessage("Telefone celular do titular do cartão é obrigatório");

        RuleFor(x => x.HolderPostalCode)
            .NotEmpty().WithMessage("CEP do titular do cartão é obrigatório");

        RuleFor(x => x.HolderAddressNumber)
            .NotEmpty().WithMessage("Número do endereço do titular do cartão é obrigatório");

        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Número do cartão é obrigatório")
            .Must(y => CleanString.OnlyNumber(y).Length == 16).WithMessage("Número do cartão inválido");

        RuleFor(x => x.ExpiryDate)
            .NotEmpty().WithMessage("Data de validade do cartão é obrigatório");

        RuleFor(x => x.CCV)
            .NotEmpty().WithMessage("Código de segurança do cartão é obrigatório");
    }
}
