using Invite.Entities.Enums;

namespace Invite.Entities.Requests;

public class EventCreateRequest
{
    public string Name { get; set; } = default!;
    public EventTypeEnum Type { get; set; }
    public int Guests { get; set; }
}
