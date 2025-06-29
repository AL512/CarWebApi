using FluentValidation;

namespace CarWebApi.CQRS.Queries.Countries;

public class GetCountryListPagedQueryValidator : AbstractValidator<GetCountryListPagedQuery>
{
    public GetCountryListPagedQueryValidator()
    {
    }
}