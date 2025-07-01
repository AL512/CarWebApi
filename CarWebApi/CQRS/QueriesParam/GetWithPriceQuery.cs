using CarWebApi.PageParams;

namespace CarWebApi.CQRS.QueriesParam;

public record GetWithPriceQuery(
    decimal minPrice,
    decimal maxPrice,
    PaginationParams paginationParams,
    string sort);