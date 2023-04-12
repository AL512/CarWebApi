using AutoMapper.QueryableExtensions;
using CarWebApi.Database;
using CarWebApi.Exceptions;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Cars
{
    /// <summary>
    /// Обработчик запроса автомобиля
    /// </summary>
    public class GetCarDetailsQueryHandler : IRequestHandler<GetCarDetailsQuery, CarDetails>
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
        /// Конструктор обработчика запроса автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public GetCarDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (UnitOfWork, Mapper) = (unitOfWork, mapper);

        /// <summary>
        /// Логика обработки запроса автомобиля
        /// </summary>
        /// <param name="request">Ответ на запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Автомобиль</returns>
        public async Task<CarDetails> Handle(GetCarDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Cars.GetById(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Car), request.Id);
            }
            return Mapper.Map<CarDetails>(entity);
        }
    }
}