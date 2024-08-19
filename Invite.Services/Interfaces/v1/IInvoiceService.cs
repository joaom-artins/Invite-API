using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface IInvoiceService
{
    Task<IEnumerable<InvoiceResponse>> FindByUserAsync();
    Task<InvoiceResponse> GetByReferenceAsync(string reference);
    Task<bool> CreateAsync(Guid userId, bool isAutomated, EventModel? eventModel = null, BuffetModel? buffet = null, HallModel? hall = null, CerimonialistModel? cerimonialist = null);
    Task<bool> PayAsync(Guid id, InvoicePayRequest request);
}
