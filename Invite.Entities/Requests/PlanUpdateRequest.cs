namespace Invite.Entities.Requests;

public class PlanUpdateRequest
{
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
}
