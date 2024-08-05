using FluentValidation;
using Invite.Entities.Enums;
using Invite.Entities.Requests;

namespace Invite.Entities.Validators;

public class InvoicePayRequestValidator : AbstractValidator<InvoicePayRequest>
{
    public InvoicePayRequestValidator()
    {
        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("Método de pagamento é um campo obrigatório!")
            .IsInEnum();

        RuleFor(x => x.CreditCard)
            .NotEmpty()
            .WithMessage("Informações do cartão são obrigatórias")
            .When(x => x.PaymentMethod == PaymentMethodEnum.CreditCard)
            .SetValidator(new CreditCardRequestValidator()!);
    }
}
