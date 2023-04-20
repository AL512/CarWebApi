using AutoMapper.QueryableExtensions;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.CQRS.Queries.Brands
{
    /// <summary>
    /// Обработчик запроса списка марок автомобиля
    /// </summary>
    public class GetBrandListQueryHandler : IRequestHandler<GetBrandListQuery, BrandList>
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
        /// Конструктор обработчика запроса списка марок автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        /// <param name="mapper">Маппер</param>
        public GetBrandListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) =>
            (UnitOfWork, Mapper) = (unitOfWork, mapper);

        /// <summary>
        /// Логика обработки запроса списка марок автомобилей
        /// </summary>
        /// <param name="request">Ответ на запрос</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список марок автомобиля</returns>
        public async Task<BrandList> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
        {
            var brandQuery = await UnitOfWork.Brands.GetAll()
                .ProjectTo<BrandLookupDto>(Mapper.ConfigurationProvider) // Расширение из AutoMapper
                .ToListAsync(cancellationToken);

            return new BrandList { Brands = brandQuery };
        }
    }
}
