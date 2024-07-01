namespace api.Entities.Models;

public class ResponsibleModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public byte PersonsInFamily { get; set; }
    public string CPF { get; set; } = default!;
}
