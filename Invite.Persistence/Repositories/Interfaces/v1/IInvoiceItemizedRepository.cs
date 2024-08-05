using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IInvoiceItemizedRepository : IGenericRepository<InvoiceItemizedModel>
{
    Task<InvoiceItemizedModel> GetByInvoiceWithIncludesAsync(Guid invoiceId);
}
