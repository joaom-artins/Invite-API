namespace Invite.Business.Interfaces.v1;

public interface IInviteBusiness
{
    Task<bool> ValidateDateAsync(Guid eventId, DateOnly limitDate);
}