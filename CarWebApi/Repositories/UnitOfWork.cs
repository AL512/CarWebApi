using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Repositories;

public class UnitOfWork<TContext>(TContext context, IServiceProvider serviceProvider) : IUnitOfWorkCarApi
    where TContext : DbContext
{
    private bool _disposed;

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var repo =  serviceProvider.GetRequiredService<IGenericRepository<TEntity>>();
        return repo;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        await context.Database.RollbackTransactionAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore();
        Dispose(false);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            context.Dispose();
        }

        _disposed = true;
    }

    private async ValueTask DisposeAsyncCore()
    {
        if (!_disposed)
        {
            await context.DisposeAsync();
        }
    }
}