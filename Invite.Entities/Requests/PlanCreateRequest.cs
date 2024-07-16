using Invite.Entities.Enums;

namespace Invite.Entities.Requests;

public class PlanCreateRequest
{
    public string Name { get; set; } = default!;
    public PlanTypeEnum Type { get; set; }
    public decimal Price { get; set; }
    public int MaxGuest { get; set; }
}
