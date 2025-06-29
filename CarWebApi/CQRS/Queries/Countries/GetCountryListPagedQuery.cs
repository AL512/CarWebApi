using CarWebApi.Models.Countries;
using CarWebApi.PageParams;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Countries;

public record GetCountryListPagedQuery(
    CountryFilterParams? Filter = null,
    SortParams? Sort = null,
    PaginationParams? Pagination = null) : IRequest<PagedRequest<CountryList>>;