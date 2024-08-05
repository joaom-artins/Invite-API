using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class InvoiceItemizedRepository(
    AppDbContext context
) : GenericRepository<InvoiceItemizedModel>(context),
    IInvoiceItemizedRepository
{
    private readonly AppDbContext _context = context;

    public async Task<InvoiceItemizedModel> GetByInvoiceWithIncludesAsync(Guid invoiceId)
    {
        var record = await _context.InvoiceItemizeds.Include(x => x.Buffet).Include(x => x.Hall).Include(x => x.Event).SingleOrDefaultAsync(x => x.InvoiceId == invoiceId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
}
