namespace Invite.Entities.Responses;

public class BuffetResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string CNPJ { get; set; } = default!;
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ServeInRadius { get; set; } = default!;
    public double Rate { get; set; }
}