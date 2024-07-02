namespace api.Persistence.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(T t);
    bool Update(T t);
    bool Remove(T t);
    bool RemoveRange(IEnumerable<T> t);
}
