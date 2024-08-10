using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class LeadModel
{
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
