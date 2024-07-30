namespace Invite.Entities.Requests;

public class ResponsibleCreateRequest
{
    public string Name { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public byte PersonInFamily { get; set; }
}
