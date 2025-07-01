using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarListPagedQuery, PagedRequest<CarLookupDto>>
{
    public async Task<PagedRequest<CarLookupDto>> Handle(GetCarListPagedQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Car>();
        var pagedResult  = await repository.GetPagedAsync(
            request.Pagination,
            request.Sort,
            request.Filter,
            cancellationToken);

        if (pagedResult == null || pagedResult.Items == null)
        {
            throw new NotFoundException(nameof(PagedRequest<CarLookupDto>), "No cars found");
        }
        
        var dtos = mapper.Map<List<CarLookupDto>>(pagedResult.Items);
        
        var result = new PagedRequest<CarLookupDto>
        {
            Items = dtos,
            TotalCount = pagedResult.TotalCount,
            PageNumber = request.Pagination.PageNumber,
            PageSize = request.Pagination.PageSize
        };
        
        return result;
    }
}