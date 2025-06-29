using System.Linq.Expressions;
using CarWebApi.Supports;

namespace CarWebApi.Extensions;

public static class ExpressionExtensions
{
    public static Expression<T> Compose<T>(
        this Expression<T> first,
        Expression<T> second,
        Func<Expression, Expression, Expression> merge)
    {
        var map = first.Parameters
            .Select((f, i) => new { f, s = second.Parameters[i] })
            .ToDictionary(p => p.s, p => p.f);

        var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

        return Expression.Lambda<T>(
            merge(first.Body, secondBody), first.Parameters);
    }
}