using CarWebApi.CQRS.Queries.Brands;
using CarWebApi.Database;
using CarWebApi.Models.Brands;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Brands.Queries
{
    /// <summary>
    /// Тестирование обработчика запроса марки автомобиля
    /// </summary>
    [Collection("QueryCollection")]
    public class GetBrandDetailsQueryHandlerTests
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
        /// Тестирование обработчика запроса марки автомобиля
        /// </summary>
        /// <param name="fixture">Вспомогательный класс проверки запросов</param>
        public GetBrandDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Mapper = fixture.Mapper;
            UnitOfWork = fixture.UnitOfWork;
        }

        /// <summary>
        /// Проверяет успешное получение марки автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetBrandDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetBrandDetailsQueryHandler(UnitOfWork, Mapper);

            // Act
            var result = await handler.Handle(
                new GetBrandDetailsQuery
                {
                    Id = Guid.Parse("57E34833-8697-4534-A2B7-C2CD49259F91"),
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BrandDetails>(result);
            Assert.Equal("LADA", result.Name);
            Assert.Equal(Guid.Parse("C0D63113-83D5-4443-A303-07250D2E0F75"), result.CountryId);

            Assert.Equal(DateTime.Today, result.CreationDate);
        }
    }
}
