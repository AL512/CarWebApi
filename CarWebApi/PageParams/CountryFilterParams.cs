using System.Linq.Expressions;
using CarWebApi.Extensions;
using CarWebApi.Models.Countries;

namespace CarWebApi.PageParams;

public class CountryFilterParams : FilterParams<Country>
{
    private readonly List<Expression<Func<Country, bool>>> _filters = new();
    
    public CountryFilterParams WithName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _filters.Add(country => country.Name.Contains(name));
        }

        return this;
    }
    
    public override Expression<Func<Country, bool>> GetFilterExpression()
    {
        var expression = _filters.Aggregate(
            (Expression<Func<Country, bool>>)(brand => true),
            (current, filter) => current.Compose(filter, Expression.AndAlso)
        );

        return expression;
    }
}