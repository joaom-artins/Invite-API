namespace api.Entities.Request;

public class ResponsibleCreateRequest
{
    public string Name { get; set; } = default!;
    public byte PersonsInFamily { get; set; }
    public string CPF { get; set; } = default!;
}
