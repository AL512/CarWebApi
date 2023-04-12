using CarWebApi.CQRS.Queries.Brands;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Brands.Queries
{
    /// <summary>
    /// Тестирование обработчика запроса списка марок автомобилей
    /// </summary>
    [Collection("QueryCollection")]
    public class GetBrandListQueryHandlerTests
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
        /// Тестирование обработчика запроса списка марок автомобилей
        /// </summary>
        /// <param name="fixture">Вспомогательный класс проверки запросов</param>
        public GetBrandListQueryHandlerTests(QueryTestFixture fixture)
        {
            UnitOfWork = fixture.UnitOfWork;
            Mapper = fixture.Mapper;
        }

        /// <summary>
        /// Проверяет успешное получение списка марок автомобилей
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetBrandQueryHandler_Success()
        {
            // Arrange
            var handler = new GetBrandListQueryHandler(UnitOfWork, Mapper);

            // Act
            var result = await handler.Handle(
                new GetBrandListQuery()
                { },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BrandList>(result);
            Assert.Equal(4, result?.Brands?.Count());
        }
    }
}
