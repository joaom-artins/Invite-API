using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class ResponsibleModel
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(80)]
    public string Name { get; set; } = default!;
    public byte PersonsInFamily { get; set; }
    [MaxLength(11)]
    public string CPF { get; set; } = default!;
    public Guid EventId { get; set; }
    public EventModel Event { get; set; } = default!;
}
