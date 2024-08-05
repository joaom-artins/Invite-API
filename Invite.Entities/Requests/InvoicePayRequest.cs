using Invite.Entities.Enums;

namespace Invite.Entities.Requests;

public class InvoicePayRequest
{
    public PaymentMethodEnum PaymentMethod { get; set; } = default!;
    public CreditCardRequest? CreditCard { get; set; }
}
