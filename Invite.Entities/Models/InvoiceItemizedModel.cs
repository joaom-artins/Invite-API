using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Invite.Entities.Models;

public class InvoiceItemizedModel
{
    public Guid Id { get; set; }
    [Precision(6,2)]
    public decimal Price { get; set; }
    [MaxLength(30)]
    public string Title { get; set; } = default!;
    [MaxLength(60)]
    public string Description { get; set; } = default!;
    public DateOnly StarDate { get; set; }
    public DateOnly FinishDate { get; set; }
    public Guid InvoiceId { get; set; }
    public InvoiceModel Invoice { get; set; } = default!;
}