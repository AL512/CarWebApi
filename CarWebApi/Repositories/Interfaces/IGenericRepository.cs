using CarWebApi.PageParams;
using CarWebApi.Requests;

namespace CarWebApi.Repositories.Interfaces;

    /// <summary>
    /// Общей интерфейс для репозиториев
    /// </summary>
    /// <typeparam name="TEntity">Тип репозитория</typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PagedRequest<TEntity>> GetPagedAsyn(
            PaginationParams pagination,
            SortParams sort,
            FilterParams<TEntity>? filter,
            CancellationToken cancellationToken = default);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        void UpdateRange(IEnumerable<TEntity> entities);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<bool> ExistsAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
    }