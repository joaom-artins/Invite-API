using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class InviteModel
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public EventModel Event { get; set; } = default!;
    [MaxLength(120)]
    public string Message { get; set; } = default!;
    public bool Acepted { get; set; } = false;
    public bool Active { get; set; } = true;
    [MaxLength(8)]
    public string Reference { get; set; } = default!;
    public DateOnly LimitDate { get; set; }
    public Guid FutureResponsibleId { get; set; }
}