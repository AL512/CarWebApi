using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Cars;

public class GetCarListPagedQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCarListPagedQuery, PagedRequest<CarList>>
{
    public async Task<PagedRequest<CarList>> Handle(GetCarListPagedQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Car>();
        var entitys = await repository.GetPagedAsyn(
            request.Pagination,
            request.Sort,
            request.Filter,
            cancellationToken);

        if (entitys == null)
        {
            throw new NotFoundException(nameof(CarList), "CarList");
        }

        return mapper.Map<PagedRequest<CarList>>(entitys);
    }
}