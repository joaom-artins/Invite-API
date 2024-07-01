using System.ComponentModel.DataAnnotations;

namespace api.Entities.Models;

public class PersonModel
{
    public Guid Id { get; set; }
    [MaxLength(80)]
    public string Name { get; set; } = default!;
    [MaxLength(11)]
    public string CPF { get; set; } = default!;
    public Guid ResponsibleId { get; set; }
    public ResponsibleModel Responsible { get; set; } = default!;
}
