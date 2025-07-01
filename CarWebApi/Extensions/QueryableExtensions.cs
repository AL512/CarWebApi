using CarWebApi.PageParams;
using System.Linq.Dynamic.Core;

namespace CarWebApi.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, PaginationParams pagination)
    {
        return query
            .Skip((pagination.PageNumber - 1) * pagination.PageSize)
            .Take(pagination.PageSize);
    }
    
    public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> query, SortParams sortParams)
    {
        if (string.IsNullOrWhiteSpace(sortParams.SortBy))
        {
            return query;
        }

        return sortParams.IsAscending
            ? query.OrderBy(sortParams.SortBy) 
            : query.OrderBy($"{sortParams.SortBy} DESC"); 
    }
    
    public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> query, FilterParams<TEntity>? filter)
    {
        return filter == null 
            ? query 
            : query.Where(filter.GetFilterExpression());
    }
}