using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class CerimonialistModel
{
    public Guid Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    [Precision(1, 1)]
    public double Rate { get; set; }
    [Precision(6, 2)]
    public decimal StartPrice { get; set; }
    [MaxLength(40)]
    public string City { get; set; } = default!;
    [MaxLength(30)]
    public string State { get; set; } = default!;
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
}
