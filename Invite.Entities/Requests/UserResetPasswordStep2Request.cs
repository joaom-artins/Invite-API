namespace Invite.Entities.Requests;

public class UserResetPasswordStep2Request
{
    public string Email { get; set; } = default!;
    public string Code { get; set; } = default!;
}