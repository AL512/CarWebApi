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
        /// Проверяет успешное создание марки автомобиля 
        /// с указанием ИД страны производителя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateBrandByCountryIdCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateBrandCommandHandler(UnitOfWork);

            string brandName = "GAZ";
            Guid countryId = Guid.Parse("C0D63113-83D5-4443-A303-07250D2E0F75");

            // Act
            var brandId = await handler.Handle(
                new CreateBrandCommand
                {
                    Name = brandName,
                    CountryId = countryId,
                },
                CancellationToken.None);

            var result = await UnitOfWork.Brands.GetById(brandId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(brandName, result.Name);
            Assert.Equal(countryId, result.Country.Id);
        }

    }
}
