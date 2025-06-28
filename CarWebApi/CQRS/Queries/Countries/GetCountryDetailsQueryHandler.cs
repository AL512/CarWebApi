using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Queries.Countries;

/// <summary>
/// Обработчик детального запроса страны производителя
/// </summary>
public class GetCountryDetailsQueryHandler(IUnitOfWorkCarApi unitOfWork, IMapper mapper)
    : IRequestHandler<GetCountryDetailsQuery, CountryDetails>
{
    public async Task<CountryDetails> Handle(GetCountryDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var repository = unitOfWork.GetRepository<Country>();
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null)
        {
            throw new NotFoundException(nameof(Country), request.Id);
        }

        return mapper.Map<CountryDetails>(entity);
    }
}