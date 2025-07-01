using FluentValidation;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedWithPowerQueryValidator : AbstractValidator<GetCarListPagedWithPowerQuery>
{
    public GetCarListPagedWithPowerQueryValidator()
    {
    }
}