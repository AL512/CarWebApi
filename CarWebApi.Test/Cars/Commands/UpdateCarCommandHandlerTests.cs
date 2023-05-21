using CarWebApi.CQRS.Commands.Cars;
using CarWebApi.Exceptions;
using CarWebApi.Models.Brands;
using CarWebApi.Models.Countries;
using CarWebApi.Test.Common;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Test.Cars.Commands
{
    /// <summary>
    /// Тестирование обработчика команд обновления автомобиля
    /// </summary>
    public class UpdateCarCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное обновления автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateCarCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateCarCommandHandler(UnitOfWork);

            Country updateCountry = new Country() { Name = "Korea" };
            Brand updateBrand = new Brand() { Name = "KIA", Country = updateCountry };
            string updateName = "Ceed";
            int updatePow = 128;
            int updateLong = 4340;
            decimal updatePrice = 1039;

            // Act
            await handler.Handle(
                new UpdateCarCommand
                {
                    Id = СarApiContextFactory.CarIdForUpdate,
                    Name = updateName,
                    Brand = updateBrand,
                    Pow = updatePow,
                    Long = updateLong,
                    Price = updatePrice,
                }, CancellationToken.None);

            // Assert
            var car = await UnitOfWork.Cars.GetById(СarApiContextFactory.CarIdForUpdate, CancellationToken.None);
            Assert.NotNull(car);
            Assert.Equal(СarApiContextFactory.CarIdForUpdate, car.Id);
            Assert.Equal(updateName, car.Name);
            Assert.Equal(updatePow, car.Pow);
            Assert.Equal(updateLong, car.Long);
            Assert.Equal(updatePrice, car.Price);

            Assert.True(car.Brand.Name.Equals(updateBrand.Name));
            Assert.True(car.Brand.Country.Name.Equals(updateCountry.Name));
        }
        /// <summary>
        /// Проверяет успешное обновления автомобиля
        /// с указанием ИД марки автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateCarByBrandIdCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateCarCommandHandler(UnitOfWork);

            string updateName = "Ceed";
            int updatePow = 128;
            int updateLong = 4340;
            decimal updatePrice = 1039;

            // Act
            await handler.Handle(
                new UpdateCarCommand
                {
                    Id = СarApiContextFactory.CarIdForUpdate,
                    Name = updateName,
                    BrandId = СarApiContextFactory.BrandIdForUpdate,
                    Pow = updatePow,
                    Long = updateLong,
                    Price = updatePrice,
                }, CancellationToken.None);

            // Assert
            var car = await UnitOfWork.Cars.GetById(СarApiContextFactory.CarIdForUpdate, CancellationToken.None);
            Assert.NotNull(car);
            Assert.Equal(СarApiContextFactory.CarIdForUpdate, car.Id);
            Assert.Equal(updateName, car.Name);
            Assert.Equal(updatePow, car.Pow);
            Assert.Equal(updateLong, car.Long);
            Assert.Equal(updatePrice, car.Price);
            Assert.Equal(СarApiContextFactory.BrandIdForUpdate, car.Brand.Id);
        }
        /// <summary>
        /// Проверяет появления исключения "NotFoundException"
        /// при обновлении автомобиля с ID, которого нет в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateCarCommandHandler_FailWrongId()
        {
            // Arrange
            var handler = new UpdateCarCommandHandler(UnitOfWork);

            // Act
            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () =>
            await handler.Handle(
                new UpdateCarCommand
                {
                    Id = Guid.NewGuid(),
                },
                CancellationToken.None));
        }
    }
}
