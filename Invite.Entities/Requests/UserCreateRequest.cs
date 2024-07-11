namespace Invite.Entities.Requests;

public class UserCreateRequest
{
    public string FullName { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
