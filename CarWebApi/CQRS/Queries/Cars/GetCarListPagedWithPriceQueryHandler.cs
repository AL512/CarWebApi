using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedWithPriceQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarListPagedWithPriceQuery, PagedRequest<CarLookupWithPriceDto>>
{
    public async Task<PagedRequest<CarLookupWithPriceDto>> Handle(
        GetCarListPagedWithPriceQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Car>();
        
        
        var pagedResult = await repository.GetPagedAsync(
            request.Pagination,
            request.Sort,
            request.Filter,
            cancellationToken: cancellationToken);

        if (pagedResult?.Items == null)
        {
            throw new NotFoundException(nameof(GetCarListPagedWithPriceQuery) , "Cars not found");
        }

        var dtos = mapper.Map<List<CarLookupWithPriceDto>>(pagedResult.Items);
        
        return new PagedRequest<CarLookupWithPriceDto>
        {
            Items = dtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = request.Pagination.PageNumber,
            PageSize = request.Pagination.PageSize
        };
    }
}