namespace Invite.Entities.Requests;

public class UserUpdateProfileRequest
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
