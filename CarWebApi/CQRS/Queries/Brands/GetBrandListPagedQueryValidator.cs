using FluentValidation;

namespace CarWebApi.CQRS.Queries.Brands;

public class GetBrandListPagedQueryValidator : AbstractValidator<GetBrandListPagedQuery>
{
    public GetBrandListPagedQueryValidator()
    {
    }
}