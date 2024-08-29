namespace Invite.Entities.Models;

public class UserRefreshTokenModel
{
    public Guid Id { get; set; }
    public string Token { get; set; } = default!;
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
}
