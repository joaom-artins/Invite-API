using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class HallModel
{
    public Guid Id { get; set; }
    [MaxLength(60)]
    public string Name { get; set; } = default!;
    [MaxLength(11)]
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
    [MaxLength(11)]
    public string OwnerNumber { get; set; } = default!;
    [MaxLength(40)]
    public string City { get; set; } = default!;
    [MaxLength(30)]
    public string State { get; set; } = default!;
    [MaxLength(50)]
    public string Street { get; set; } = default!;
    [MaxLength(5)]
    public string Number { get; set; } = default!;
    [MaxLength(8)]
    public string CEP { get; set; } = default!;
    [Precision(6, 2)]
    public decimal PriceInWeek { get; set; }
    [Precision(6, 2)]
    public decimal PriceInWeekend { get; set; }
    public bool Paid { get; set; } = false;
    public int Rate { get; set; } = 0;
}
