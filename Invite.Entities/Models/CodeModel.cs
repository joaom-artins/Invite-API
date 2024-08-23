using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class CodeModel
{
    public Guid Id { get; set; }
    [MaxLength(6)]
    public string Code { get; set; } = default!;
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
