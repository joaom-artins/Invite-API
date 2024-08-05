using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IInvoiceService
{
    Task<bool> CreateAsync(EventModel? eventModel = null, BuffetModel? buffet = null, HallModel? hall = null);
    Task<bool> PayAsync(Guid id, InvoicePayRequest request);
}
