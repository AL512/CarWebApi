using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedWithPowerQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarListPagedWithPowerQuery, PagedRequest<CarLookupWithPowerDto>>
{
    public async Task<PagedRequest<CarLookupWithPowerDto>> Handle(
        GetCarListPagedWithPowerQuery request,
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
            throw new NotFoundException(nameof(GetCarListPagedWithPowerQuery) , "Cars not found");
        }

        var dtos = mapper.Map<List<CarLookupWithPowerDto>>(pagedResult.Items);
        
        return new PagedRequest<CarLookupWithPowerDto>
        {
            Items = dtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = request.Pagination.PageNumber,
            PageSize = request.Pagination.PageSize
        };
    }
}