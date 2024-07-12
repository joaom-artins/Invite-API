namespace Invite.Business.Interfaces.v1;

public interface IPlanBusiness
{
    Task<bool> ExistsNameAsync(string name);
}
