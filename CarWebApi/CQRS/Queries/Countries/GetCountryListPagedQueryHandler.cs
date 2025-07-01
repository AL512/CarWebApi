using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Requests;

namespace CarWebApi.CQRS.Queries.Countries;

public class GetCountryListPagedQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCountryListPagedQuery, PagedRequest<CountryList>>
{
    public async Task<PagedRequest<CountryList>> Handle(GetCountryListPagedQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Country>();
        var entitys = await repository.GetPagedAsync(
            request.Pagination,
            request.Sort,
            request.Filter,
            cancellationToken);

        if (entitys == null)
        {
            throw new NotFoundException(nameof(CountryList), "CountryList");
        }

        return mapper.Map<PagedRequest<CountryList>>(entitys);
    }
}