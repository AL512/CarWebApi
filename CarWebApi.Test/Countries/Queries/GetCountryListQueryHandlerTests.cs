using CarWebApi.CQRS.Queries.Countries;
using CarWebApi.Database;
using CarWebApi.Models.Countries;
using CarWebApi.Queries.Countries;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Countries.Queries
{
    /// <summary>
    /// Тестирование обработчика запроса списка стран производителей
    /// </summary>
    [Collection("QueryCollection")]
    public class GetCountryListQueryHandlerTests
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
        /// Тестирование обработчика запроса списка стран производителей
        /// </summary>
        /// <param name="fixture">Вспомогательный класс проверки запросов</param>
        public GetCountryListQueryHandlerTests(QueryTestFixture fixture)
        {
            UnitOfWork = fixture.UnitOfWork;
            Mapper = fixture.Mapper;
        }

        /// <summary>
        /// Проверяет успешное получение списка стран производителей
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetCountryQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCountryListQueryHandler(UnitOfWork, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCountryListQuery()
                { },             
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CountryList>(result);
            Assert.Equal(4, result?.Countries?.Count());
        }
    }
}
