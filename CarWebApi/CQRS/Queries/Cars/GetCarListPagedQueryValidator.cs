using FluentValidation;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedQueryValidator : AbstractValidator<GetCarListPagedQuery>
{
    public GetCarListPagedQueryValidator()
    {
    }
}