using System.ComponentModel.DataAnnotations;
using Invite.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class PlanModel
{
    public Guid Id { get; set; }
    [MaxLength(40)]
    public string Name { get; set; } = default!;
    [Precision(6, 2)]
    public decimal Price { get; set; }
    public PlanTypeEnum Type { get; set; }
    public int MaxGuest { get; set; }
}
