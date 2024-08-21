namespace Invite.Services.Interfaces.v1;

public interface ICodeService
{
    Task<string> CreateAsync(Guid userId);
}
