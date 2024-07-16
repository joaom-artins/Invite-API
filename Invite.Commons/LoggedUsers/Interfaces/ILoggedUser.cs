namespace Invite.Commons.LoggedUsers.Interfaces;

public interface ILoggedUser
{
    Guid GetId();
    string GetRole();
}
