using System.ComponentModel.DataAnnotations;
using Invite.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class InvoiceModel
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;
    [Precision(6, 2)]
    public decimal Price { get; set; }
    [Precision(6, 2)]
    public decimal Discount { get; set; }
    [Precision(6, 2)]
    public decimal Total { get; set; }
    [MaxLength(6)]
    public string Reference { get; set; } = default!;
    public string? ExternalId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateOnly DueDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public InvoiceStatusEnum Status { get; set; }
    public PaymentMethodEnum PaymentMethod { get; set; }
}
