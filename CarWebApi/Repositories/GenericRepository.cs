using CarWebApi.Extensions;
using CarWebApi.PageParams;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public virtual async Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PagedRequest<TEntity>> GetPagedAsyn(PaginationParams pagination, SortParams sort,
        FilterParams<TEntity>? filter,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.AsNoTracking().AsQueryable();

        var filtered = query?.Filter(filter);
        int totalCount = await filtered?.CountAsync(cancellationToken)!;

        var sorted = filtered.Sort(sort);
        var paginated = sorted.Paginate(pagination);

        var items = await paginated.ToListAsync(cancellationToken);

        return new PagedRequest<TEntity>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pagination.PageNumber,
            PageSize = pagination.PageSize
        };
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Remove(entity);
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<bool> ExistsAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        var getByIdResult = await GetByIdAsync(id, cancellationToken) != null;
        return getByIdResult;
    }
}