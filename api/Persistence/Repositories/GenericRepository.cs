using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories;

public class GenericRepository<T>(
    AppDbContext _context
) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }

    public async Task<bool> AddAsync(T t)
    {
        await _context.Set<T>().AddAsync(t);

        return true;
    }

    public bool Update(T t)
    {
        _context.Set<T>().Update(t);

        return true;
    }

    public bool Remove(T t)
    {
        _context.Set<T>().Remove(t);

        return true;
    }

    public bool RemoveRange(IEnumerable<T> t)
    {
        _context.Set<T>().RemoveRange(t);

        return true;
    }
}
