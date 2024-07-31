namespace Invite.Entities.Requests;

public class BuffetUpdateRequest
{
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string CNPJ { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ServeInRadius { get; set; } = default!;
}
