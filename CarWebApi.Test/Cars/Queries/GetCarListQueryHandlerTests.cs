using CarWebApi.CQRS.Queries.Cars;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Cars.Queries
{
    /// <summary>
    /// Тестирование обработчика запроса списка автомобилей
    /// </summary>
    [Collection("QueryCollection")]
    public class GetCarListQueryHandlerTests
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
        /// Тестирование обработчика запроса списка автомобилей
        /// </summary>
        /// <param name="fixture">Вспомогательный класс проверки запросов</param>
        public GetCarListQueryHandlerTests(QueryTestFixture fixture)
        {
            UnitOfWork = fixture.UnitOfWork;
            Mapper = fixture.Mapper;
        }

        /// <summary>
        /// Проверяет успешное получение списка автомобилей
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetCarQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCarListQueryHandler(UnitOfWork, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCarListQuery
                { },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CarList>(result);
            Assert.Equal(5, result?.Cars?.Count());
        }
    }
}
