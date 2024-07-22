namespace Invite.Entities.Responses;

public class PersonResponse
{
    public string Name { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public Guid ResponsibleId { get; set; }
}
