using CarWebApi.CQRS.Queries.Cars;
using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Brands;

public class GetBrandListPagedQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetBrandListPagedQuery, PagedRequest<BrandList>>
{

    public async Task<PagedRequest<BrandList>> Handle(GetBrandListPagedQuery request, CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Brand>();
        var entitys = await repository.GetPagedAsyn(
            request.Pagination,
            request.Sort,
            request.Filter,
            cancellationToken);

        if (entitys == null)
        {
            throw new NotFoundException(nameof(BrandList), "BrandList");
        }

        return mapper.Map<PagedRequest<BrandList>>(entitys);
    }
}