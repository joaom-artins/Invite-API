namespace Invite.Services.Interfaces.v1;

public interface IServiceService
{
    Task<bool> CreateAsync(Guid userId);
}
