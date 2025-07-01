using CarWebApi.Models.Cars;
using CarWebApi.PageParams;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Cars;

public record GetCarListPagedWithPriceQuery(
    CarFilterParams? Filter = null,
    SortParams? Sort = null,
    PaginationParams? Pagination = null) : IRequest<PagedRequest<CarLookupWithPriceDto>>;
    