using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class InvoiceRepository(
    AppDbContext context
) : GenericRepository<InvoiceModel>(context),
    IInvoiceRepository
{
    private readonly AppDbContext _context = context;

    public async Task<InvoiceModel> GetByIdWithUserAsync(Guid id)
    {
        var record = await _context.Invoices.Include(x => x.User).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        if(record is null)
        {
            return default!;
        }

        return record;
    }

    public async Task<InvoiceModel> GetByUserAndReferenceAsync(Guid userId, string reference)
    {
        var record = await _context.Invoices.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId && x.Reference == reference);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
}
