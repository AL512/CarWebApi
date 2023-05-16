using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Models.Countries;
using CarWebApi.Test.Common;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Test.Brands.Commands
{
    /// <summary>
    /// Тестирование обработчика команд создания марки автомобиля
    /// </summary>
    public class CreateBrandCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное создание марки автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBrandCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateBrandCommandHandler(UnitOfWork);

            string brandName = "Geely";
            Country country = new Country() { Name = "China" };

            // Act
            var brandId = await handler.Handle(
                new CreateBrandCommand
                {
                    Name = brandName,
                    Country = country,
                },
                CancellationToken.None);

            var result = await UnitOfWork.Brands.GetById(brandId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(brandName, result.Name);
            Assert.Equal(country.Name, result.Country.Name);
        }

        /// <summary>
        /// Проверяет успешное создание марки автомобиля в короткой форме
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBrandShortCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateBrandCommandHandler(UnitOfWork);

            string brandName = "Genesis";

            // Act
            var brandId = await handler.Handle(
                new CreateBrandCommand
                {
                    Name = brandName,
                    CountryId = СarApiContextFactory.CountryIdForUpdate,
                },
                CancellationToken.None);

            var result = await UnitOfWork.Brands.GetById(brandId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(brandName, result.Name);
            Assert.Equal(СarApiContextFactory.CountryIdForUpdate, result.Country.Id);
            Assert.Equal("Korea", result.Country.Name);

        }

    }
}
