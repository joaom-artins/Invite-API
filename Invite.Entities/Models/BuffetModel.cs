using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class BuffetModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    [MaxLength(11)]
    public string PhoneNumber { get; set; } = default!;
    [MaxLength(14)]
    public string CNPJ { get; set; } = default!;
    [MaxLength(40)]
    public string City { get; set; } = default!;
    [MaxLength(30)]
    public string State { get; set; } = default!;
    [MaxLength(4)]
    public string ServeInRadius { get; set; } = default!;
    public bool Paid { get; set; } = false;
    [Precision(1,1)]
    public double Rate { get; set; } = 0;
}