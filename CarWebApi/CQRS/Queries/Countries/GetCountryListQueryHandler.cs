using AutoMapper.QueryableExtensions;
using CarWebApi.Database;
using CarWebApi.Models.Countries;
using CarWebApi.Queries.Countries;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Countries
{
    /// <summary>
    /// Обработчик запроса списка стран производителей
    /// </summary>
    public class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, CountryList>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;
        /// <summary>
        /// Маппер
        /// </summary>
        private readonly IMapper Mapper;
        /// <summary>
        /// Обработчик запроса списка стран производителей
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        /// <param name="mapper">Маппер</param>
        public GetCountryListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
           (UnitOfWork, Mapper) = (unitOfWork, mapper);
        /// <summary>
        /// Логика обработки запроса списка
        /// </summary>
        /// <param name="request">Ответ на запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список автомобилей</returns>
        public async Task<CountryList> Handle(GetCountryListQuery request,
            CancellationToken cancellationToken)
        {
            var countriesEnumer = await UnitOfWork.Countries.GetAll(cancellationToken);

            var countriesList = countriesEnumer.AsQueryable()
                    .ProjectTo<CountryLookupDto>(Mapper.ConfigurationProvider)
                    .ToList();

            return new CountryList { Countries = countriesList };
        }
    }
}
