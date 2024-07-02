using System.ComponentModel.DataAnnotations;

namespace Invite.Entities.Models;

public class PersonModel
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(80)]
    public string Name { get; set; } = default!;
    [MaxLength(11)]
    public string CPF { get; set; } = default!;
    public Guid ResponsibleId { get; set; }
    public ResponsibleModel Responsible { get; set; } = default!;
}
