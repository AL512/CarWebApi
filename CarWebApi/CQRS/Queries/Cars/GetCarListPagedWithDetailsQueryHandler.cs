using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedWithDetailsQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarListPagedWithDetailsQuery, PagedRequest<CarLookupWithDetailsDto>>
{
    public async Task<PagedRequest<CarLookupWithDetailsDto>> Handle(
        GetCarListPagedWithDetailsQuery request,
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
            throw new NotFoundException(nameof(GetCarListPagedWithDetailsQuery) , "Cars not found");
        }

        var dtos = mapper.Map<List<CarLookupWithDetailsDto>>(pagedResult.Items);
        
        return new PagedRequest<CarLookupWithDetailsDto>
        {
            Items = dtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = request.Pagination.PageNumber,
            PageSize = request.Pagination.PageSize
        };
    }
}