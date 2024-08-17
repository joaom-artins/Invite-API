namespace Invite.Business.Interfaces.v1;

public interface ICerimonialistBusiness
{
    Task<bool> CheckExistsByUserAsync(Guid userId);
}
