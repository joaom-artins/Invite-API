namespace Invite.Entities.Responses;

public class CerimonialistReponse
{
    public string Name { get; set; } = default!;
    public double Rate { get; set; }
    public decimal StartPrice { get; set; }
    public string City { get; set; } = default!;
    public string State { get; set; } = default!;
}
