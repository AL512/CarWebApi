namespace CarWebApi.Repositories.Interfaces;

public interface IUnitOfWork<TContext> : IDisposable, IAsyncDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}