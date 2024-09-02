namespace Invite.Entities.Models;

public class ServiceModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; } = default!;
    public Guid UserId { get; set; }
    public DateOnly? NextDueDate { get; set; }
    public int Halls { get; set; }
    public int Buffets { get; set; }
    public int Cerimonialist { get; set; }
}
