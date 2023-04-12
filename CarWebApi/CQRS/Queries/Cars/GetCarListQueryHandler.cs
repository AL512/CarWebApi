using AutoMapper.QueryableExtensions;
using CarWebApi.Database;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Обработчик запроса списка автомобиля
    /// </summary>
    public class GetCarListQueryHandler : IRequestHandler<GetCarListQuery, CarList>
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
        /// Конструктор обработчика запроса списка автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        /// <param name="mapper">Маппер</param>
        public GetCarListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (UnitOfWork, Mapper) = (unitOfWork, mapper);

        /// <summary>
        /// Логика обработки запроса списка автомобиля
        /// </summary>
        /// <param name="request">Ответ на запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список автомобилей</returns>
        public async Task<CarList> Handle(GetCarListQuery request, CancellationToken cancellationToken)
        {
            var carEnumer = await UnitOfWork.Cars.GetAll(cancellationToken);

            var CardList = carEnumer.AsQueryable()
                    .ProjectTo<CarLookupDto>(Mapper.ConfigurationProvider)
                    .ToList();

            return new CarList { Cars = CardList };
        }
    }
}
