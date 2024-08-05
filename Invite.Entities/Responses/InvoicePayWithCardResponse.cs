namespace Invite.Entities.Responses;

public class InvoicePayWithCardResponse
{
    public string Id { get; set; } = default!;
    public string Status { get; set; } = default!;
    public DateOnly ConfirmedDate { get; set; } = default!;
}
