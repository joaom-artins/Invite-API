namespace Invite.Business.Interfaces.v1;

public interface ICodeBusiness
{
    Task<bool> ValidateForCreateAsync(Guid userId);
}