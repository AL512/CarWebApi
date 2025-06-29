using System.Linq.Expressions;

namespace CarWebApi.PageParams;

public abstract class FilterParams<T>
{
    public abstract Expression<Func<T, bool>> GetFilterExpression();
}