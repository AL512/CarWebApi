namespace CarWebApi.Repositories.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
    Task RollbackAsync(CancellationToken cancellationToken = default);
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}