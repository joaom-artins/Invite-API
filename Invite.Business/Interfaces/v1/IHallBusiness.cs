namespace Invite.Business.Interfaces.v1;

public interface IHallBusiness
{
    Task<bool> ExistsByName(Guid userId, string name);
}