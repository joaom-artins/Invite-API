namespace Invite.Entities.Responses;

public class HallResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string OwnerNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string Number { get; set; } = default!;
    public string CEP { get; set; } = default!;
    public decimal PriceInWeek { get; set; }
    public decimal PriceInWeekend { get; set; }
    public double Rate { get; set; }
}
