namespace Invite.Entities.Requests;

public class CreditCardRequest
{
    public string HolderName { get; set; } = default!;
    public string HolderCpfCnpj { get; set; } = default!;
    public string HolderEmail { get; set; } = default!;
    public string HolderMobilePhone { get; set; } = default!;
    public string HolderPostalCode { get; set; } = default!;
    public string HolderAddressNumber { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string ExpiryDate { get; set; } = default!;
    public string CCV { get; set; } = default!;
}
