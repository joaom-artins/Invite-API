namespace Invite.Entities.Requests;

public class CerimonialistUpdateRequest
{
    public string Name { get; set; } = default!;
    public decimal StartPrice { get; set; }
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
}
