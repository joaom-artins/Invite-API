using api.Persistence.Context;

namespace api.Persistence.UnitOfWorks.Interfaces;

public interface IUnitOfWork
{
    void BeginTransaction();
    Task CommitAsync(bool isFinishTransaction = false);
    void Rollback();
    Task ExecuteSqlInterpolatedAsync(FormattableString sql);
}
