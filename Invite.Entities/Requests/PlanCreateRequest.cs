using Invite.Entities.Enums;

namespace Invite.Entities.Requests;

public class PlanCreateRequest
{
    public string Name { get; set; } = default!;
    public TypePlanEnum Type { get; set; }
    public decimal Price { get; set; }
}
