using CarWebApi.CQRS.Queries.Countries;
using CarWebApi.Database;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Countries.Queries
{
    /// <summary>
    /// Тестирование обработчика запроса страны производителя
    /// </summary>
    [Collection("QueryCollection")]
    public class GetCountryDetailsQueryHandlerTests
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
        /// Тестирование обработчика запроса страны производителя
        /// </summary>
        /// <param name="fixture">Вспомогательный класс проверки запросов</param>
        public GetCountryDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            UnitOfWork = fixture.UnitOfWork;
            Mapper = fixture.Mapper;
        }

        /// <summary>
        /// Проверяет успешное получение страны производителя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetCountryDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCountryDetailsQueryHandler(UnitOfWork, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCountryDetailsQuery
                {
                    Id = Guid.Parse("C0D63113-83D5-4443-A303-07250D2E0F75"),
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CountryDetails>(result);
            Assert.Equal("Russia", result.Name);
            Assert.Equal(DateTime.Today, result.CreationDate);
        }
    }
}
