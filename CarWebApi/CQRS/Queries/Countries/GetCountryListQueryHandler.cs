using AutoMapper.QueryableExtensions;
using CarWebApi.Models.Countries;
using CarWebApi.Queries.Countries;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Countries;

/// <summary>
/// Обработчик запроса списка стран производителей
/// </summary>
public class GetCountryListQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCountryListQuery, CountryList>
{
    public async Task<CountryList> Handle(GetCountryListQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Country>();
        var countriesEnumer = await repository.GetAllAsync(cancellationToken);

        var countriesList = await countriesEnumer
            .AsQueryable()
            .ProjectTo<CountryLookupDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CountryList { Countries = countriesList };
    }
}