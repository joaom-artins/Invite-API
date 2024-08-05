using Invite.Entities.Models;

namespace Invite.Services.Interfaces.v1;

public interface IInvoiceService
{
    Task<bool> CreateAsync(EventModel? eventModel = null, BuffetModel? buffet = null, HallModel? hall = null);
}
