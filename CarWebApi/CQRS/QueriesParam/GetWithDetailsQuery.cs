using CarWebApi.PageParams;

namespace CarWebApi.CQRS.QueriesParam;

public record GetWithDetailsQuery(
    int minPower,
    int maxPower,
    decimal minPrice,
    decimal maxPrice,
    PaginationParams paginationParams,
    string sort);