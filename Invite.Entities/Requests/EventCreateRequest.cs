using Invite.Entities.Enums;

namespace Invite.Entities.Requests;

public class EventCreateRequest
{
    public string Name { get; set; } = default!;
    public EventTypeEnum Type { get; set; }
    public int Guests { get; set; }
    public DateOnly Date { get; set; }
    public bool UseHallRegistred { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? CEP { get; set; }
    public Guid? HallId { get; set; }
}
