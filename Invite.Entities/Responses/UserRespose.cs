namespace Invite.Entities.Responses;

public class UserRespose
{
    public string FullName { get; set; } = default!;
    public string CPF { get; set; } = default!;
    public int DueDay { get; set; }
    public string Email { get; set; } = default!;
}
