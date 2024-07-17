namespace Invite.Entities.Requests;

public class InviteCreateRequest
{
    public string Message { get; set; } = default!;
    public DateOnly LimitDate { get; set; }
}
