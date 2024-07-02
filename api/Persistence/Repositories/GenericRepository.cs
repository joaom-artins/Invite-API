using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories;

public class GenericRepository<T>(
    AppDbContext context
) : IGenericRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().AsNoTracking().SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
    }


    public async Task<bool> AddAsync(T t)
    {
        await context.Set<T>().AddAsync(t);

        return true;
    }

    public bool Update(T t)
    {
        context.Set<T>().Update(t);

        return true;
    }

    public bool Remove(T t)
    {
        context.Set<T>().Remove(t);

        return true;
    }

    public bool RemoveRange(IEnumerable<T> t)
    {
        context.Set<T>().RemoveRange(t);

        return true;
    }
}
