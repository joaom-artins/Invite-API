using Invite.Entities.Enums;

namespace Invite.Entities.Responses;

public class InvoiceResponse
{
    public Guid Id { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string Reference { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateOnly DueDate { get; set; }
    public DateOnly? PaymentDate { get; set; }
    public InvoiceStatusEnum Status { get; set; }
    public PaymentMethodEnum? PaymentMethod { get; set; }
}
