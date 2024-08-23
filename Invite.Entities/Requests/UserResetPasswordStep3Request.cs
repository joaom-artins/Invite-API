namespace Invite.Entities.Requests;

public class UserResetPasswordStep3Request
{
    public string Hash { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmNewPassword { get; set; } = default!;
}
