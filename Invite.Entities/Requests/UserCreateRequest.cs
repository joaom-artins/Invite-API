using Invite.Entities.Enums;

namespace Invite.Entities.Requests;

public class UserCreateRequest
{
    public string FullName { get; set; } = default!;
    public string CPFOrCNPJ { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public ClientTypeEnum TypeClient { get; set; }
}
