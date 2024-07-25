using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class HallModel
{
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string Name { get; set; } = default!;
    [MaxLength(90)]
    public string Address { get; set; } = default!;
    [Precision(6, 2)]
    public decimal PriceInWeek { get; set; }
    [Precision(6, 2)]
    public decimal PriceInWeekend { get; set; }
}
