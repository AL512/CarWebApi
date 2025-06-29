using System.Linq.Expressions;
using CarWebApi.Extensions;
using CarWebApi.Models.Cars;

namespace CarWebApi.PageParams;

public class CarFilterParams : FilterParams<Car>
{
    private readonly List<Expression<Func<Car, bool>>> _filters = new();
    
    public CarFilterParams WithName(string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            _filters.Add(car => car.Name.Contains(name));
        }

        return this;
    }

    public CarFilterParams WithPower(int? minPower, int? maxPower)
    {
        if (minPower.HasValue)
        {
            _filters.Add(car => car.Pow >= minPower.Value);
        }

        if (maxPower.HasValue)
        {
            _filters.Add(car => car.Pow <= maxPower.Value);
        }

        return this;
    }

    public CarFilterParams WithLength(int? minLength, int? maxLength)
    {
        if (minLength.HasValue)
        {
            _filters.Add(car => car.Long >= minLength.Value);
        }

        if (maxLength.HasValue)
        {
            _filters.Add(car => car.Long <= maxLength.Value);
        }

        return this;
    }

    public CarFilterParams WithCustomFilter(Expression<Func<Car, bool>> filter)
    {
        if (filter != null)
        {
            _filters.Add(filter);
        }

        return this;
    }

    public override Expression<Func<Car, bool>> GetFilterExpression()
    {
        var expression = _filters.Aggregate(
            (Expression<Func<Car, bool>>)(car => true),
            (current, filter) => current.Compose(filter, Expression.AndAlso)
        );

        return expression;
    }
}