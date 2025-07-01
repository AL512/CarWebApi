using CarWebApi.PageParams;

namespace CarWebApi.CQRS.QueriesParam;

public record GetWithPowerQuery(
    int minPower,
    int maxPower,
    PaginationParams paginationParams,
    string sort);