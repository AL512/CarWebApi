using CarWebApi.Models.Brands;
using CarWebApi.PageParams;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Brands;

public record GetBrandListPagedQuery(
    BrandFilterParams? Filter = null,
    SortParams? Sort = null,
    PaginationParams? Pagination = null) : IRequest<PagedRequest<BrandList>>;