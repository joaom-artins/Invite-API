using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Invite.Entities.Models;

public class UserModel : IdentityUser<Guid>
{
    [MaxLength(60)]
    public string FullName { get; set; } = default!;
    [MaxLength(14)]
    public string CPF { get; set; } = default!;
}
