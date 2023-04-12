using CarWebApi.CQRS.Queries.Cars;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Cars.Queries
{
    /// <summary>
    /// Тестирование обработчика запроса автомобиля
    /// </summary>
    [Collection("QueryCollection")]
    public class GetCarDetailsQueryHandlerTests
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
        /// Тестирование обработчика запроса автомобиля
        /// </summary>
        /// <param name="fixture">Вспомогательный класс проверки запросов</param>
        public GetCarDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            UnitOfWork = fixture.UnitOfWork;
            Mapper = fixture.Mapper;
        }

        /// <summary>
        /// Проверяет успешное получение автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetCarQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCarDetailsQueryHandler(UnitOfWork, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCarDetailsQuery
                {
                    Id = Guid.Parse("00385A0C-012D-4777-96E8-0D239937F087"),
                },
                CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<CarDetails>(result);
            Assert.Equal("Camry", result.Name);
            Assert.Equal(Guid.Parse("339CBD77-CDD6-44A2-9FE9-85C9CD82D9CA"), result.BrandId);
            Assert.Equal(249, result.Pow);
            Assert.Equal(4500, result.Long);
            Assert.Equal(1573, result.Price);
            Assert.Equal(DateTime.Today, result.CreationDate);

        }
    }
}
