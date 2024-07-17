using System.ComponentModel.DataAnnotations;
using Invite.Entities.Enums;
using Microsoft.AspNetCore.Identity;

namespace Invite.Entities.Models;

public class UserModel : IdentityUser<Guid>
{
    [MaxLength(60)]
    public string FullName { get; set; } = default!;
    [MaxLength(14)]
    public string CPFOrCNPJ { get; set; } = default!;
    public ClientTypeEnum TypeClient { get; set; }
    public string? ExternalId { get; set; }
    public PaymentMethodEnum? PaymentMethod { get; set; }
}
