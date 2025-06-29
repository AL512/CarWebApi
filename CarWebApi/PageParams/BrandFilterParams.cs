using System.Linq.Expressions;
using CarWebApi.Extensions;
using CarWebApi.Models.Brands;

namespace CarWebApi.PageParams;

public class BrandFilterParams : FilterParams<Brand>
{
    private readonly List<Expression<Func<Brand, bool>>> _filters = new();
    
    public BrandFilterParams WithName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _filters.Add(brand => brand.Name.Contains(name));
        }

        return this;
    }
    
    public BrandFilterParams WithCountry(string country)
    {
        if (!string.IsNullOrWhiteSpace(country))
        {
            _filters.Add(brand => brand.Country.Name.Contains(country));
        }

        return this;
    }
    
    public override Expression<Func<Brand, bool>> GetFilterExpression()
    {
        var expression = _filters.Aggregate(
            (Expression<Func<Brand, bool>>)(brand => true),
            (current, filter) => current.Compose(filter, Expression.AndAlso)
        );

        return expression;
    }
}