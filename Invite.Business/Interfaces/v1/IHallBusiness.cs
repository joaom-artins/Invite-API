namespace Invite.Business.Interfaces.v1;

public interface IHallBusiness
{
    Task<bool> ExistsByNameAsync(Guid serviceId, string name);
}