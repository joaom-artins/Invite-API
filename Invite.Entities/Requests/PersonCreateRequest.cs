namespace Invite.Entities.Requests;

public class PersonCreateRequest
{
    public string Name { get; set; } = default!;
    public string CPF { get; set; } = default!;
}
