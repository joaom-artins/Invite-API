namespace Invite.Business.Interfaces.v1;

public interface IEventBusiness
{
    Task<bool> ValidatePlanAsync(Guid planId, Guid userId, int guests);
}
