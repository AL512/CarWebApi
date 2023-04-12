using AutoMapper;
using CarWebApi.Database;
using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Countries
{
    /// <summary>
    /// Обработчик детального запроса страны производителя
    /// </summary>
    public class GetCountryDetailsQueryHandler : IRequestHandler<GetCountryDetailsQuery, CountryDetails>
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
        /// Обработчик детального запроса страны производителя
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        /// <param name="mapper">Маппер</param>
        public GetCountryDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) => 
            (UnitOfWork, Mapper) = (unitOfWork, mapper);

        /// <summary>
        /// Логика обработки запроса
        /// </summary>
        /// <param name="request">Ответ на запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Детальная информация об автомобиле</returns>
        public async Task<CountryDetails> Handle(GetCountryDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Countries.GetById(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Country), request.Id);
            }

            return Mapper.Map<CountryDetails>(entity);
        }
    }
}
