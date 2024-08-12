using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IInvoiceRepository : IGenericRepository<InvoiceModel>
{
    Task<IEnumerable<InvoiceModel>> FindByUserAsync(Guid userId);
    Task<InvoiceModel> GetByUserAndReferenceAsync(Guid userId, string reference);
}
