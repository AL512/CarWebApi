using CarWebApi.CQRS.Commands.Cars;
using CarWebApi.Models.Brands;
using CarWebApi.Models.Cars;
using CarWebApi.Models.Countries;
using CarWebApi.Test.Common;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Test.Cars.Commands
{
    /// <summary>
    /// Тестирование обработчика команд создания автомобиля
    /// </summary>
    public class CreateCarCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное создание автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateCarCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateCarCommandHandler(UnitOfWork);

            Country country = new Country() { Name = "Korea" };
            Brand brand = new Brand() { Name = "KIA", Country = country };
            string name = "Rio";
            int pow = 100;
            int _long = 4400;
            decimal price = 734;

            // Act
            var carId = await handler.Handle(
                new CreateCarCommand
                {
                    Name = name,
                    Brand = brand,
                    Pow = pow,
                    Long = _long,
                    Price = price,
                },
                CancellationToken.None);

            var result = await UnitOfWork.Cars.GetById(carId, CancellationToken.None);
            //Assert

            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(brand.Name, result.Brand.Name);
            Assert.Equal(pow, result.Pow);
            Assert.Equal(_long, result.Long);
            Assert.Equal(price, result.Price);

        }
        /// <summary>
        /// Проверяет успешное создание автомобиля
        /// с указанием ИД марки автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateCarByBrandIdCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateCarCommandHandler(UnitOfWork);

            string name = "RAV4";
            int pow = 199;
            int _long = 4600;
            decimal price = 15687;
            Guid toyotaId = Guid.Parse("339CBD77-CDD6-44A2-9FE9-85C9CD82D9CA");

            // Act
            var carId = await handler.Handle(
                new CreateCarCommand
                {
                    Name = name,
                    BrandId = toyotaId,
                    Pow = pow,
                    Long = _long,
                    Price = price,
                },
                CancellationToken.None);

            var result = await UnitOfWork.Cars.GetById(carId, CancellationToken.None);
            //Assert

            Assert.NotNull(result);
            Assert.Equal(name, result.Name);
            Assert.Equal(toyotaId, result.Brand.Id);
            Assert.Equal(pow, result.Pow);
            Assert.Equal(_long, result.Long);
            Assert.Equal(price, result.Price);

        }

    }
}
