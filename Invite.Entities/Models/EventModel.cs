using System.ComponentModel.DataAnnotations;
using Invite.Entities.Enums;

namespace Invite.Entities.Models;

public class EventModel
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string Name { get; set; } = default!;
    public EventTypeEnum Type { get; set; }
    public Guid PlanId { get; set; }
    public PlanModel Plan { get; set; } = default!;
    public int Guests { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
    public DateOnly Date { get; set; }
    [MaxLength(40)]
    public string? City { get; set; }
    [MaxLength(30)]
    public string? State { get; set; }
    [MaxLength(50)]
    public string? Street { get; set; }
    [MaxLength(5)]
    public string? Number { get; set; }
    [MaxLength(8)]
    public string? CEP { get; set; }
    public bool UseHallRegistred { get; set; }
    public Guid? HallId { get; set; }
    public HallModel? Hall { get; set; }
    public bool Paid { get; set; } = false;
}
