using Invite.Entities.Models;

namespace Invite.Entities.Responses;

public class ResponsibleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public byte PersonsInFamily { get; set; }
    public string CPF { get; set; } = default!;
    public Guid InviteId { get; set; }
    public InviteModel Invite { get; set; } = default!;
    public ICollection<PersonResponse> Persons { get; set; } = default!;
}