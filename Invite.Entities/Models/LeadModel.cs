using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class LeadModel
{
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string FullName { get; set; } = default!;
    [MaxLength(40)]
    public string Email { get; set; } = default!;
    [MaxLength(11)]
    public string PhoneNumber { get; set; } = default!;
}
