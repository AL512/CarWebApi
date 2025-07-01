using FluentValidation;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedWithPriceQueryValidator : AbstractValidator<GetCarListPagedWithPriceQuery>
{
    public GetCarListPagedWithPriceQueryValidator( )
    {
    }
}