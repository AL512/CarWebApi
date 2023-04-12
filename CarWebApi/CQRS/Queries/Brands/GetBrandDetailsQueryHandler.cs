using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Queries.Brands
{
    /// <summary>
    /// Обработчик запроса марки автомобиля
    /// </summary>
    public class GetBrandDetailsQueryHandler : IRequestHandler<GetBrandDetailsQuery, BrandDetails>
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
        /// Конструктор обработчика запроса марки автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        /// <param name="mapper">Маппер</param>
        public GetBrandDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (UnitOfWork, Mapper) = (unitOfWork, mapper);

        /// <summary>
        /// Логика обработки запроса марки автомобиля
        /// </summary>
        /// <param name="request">Ответ на запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Марка автомобиля</returns>
        public async Task<BrandDetails> Handle(GetBrandDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await UnitOfWork.Brands.GetById(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }
            return Mapper.Map<BrandDetails>(entity);
        }
    }
}