using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Exceptions;
using CarWebApi.Models.Countries;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Brands.Commands
{
    /// <summary>
    /// Тестирование обработчика команд обновления марки автомобиля
    /// </summary>
    public class UpdateBrandCommandHandlerTests : TestCommandBase   
    {
        /// <summary>
        /// Проверяет успешное обновления марки автомобиля
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateBrandCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateBrandCommandHandler(UnitOfWork);
            var updateName = "Mastretta MXT";
            var updateCountry = new Country() { Name = "Mexico" };

            // Act
            await handler.Handle(
                new UpdateBrandCommand
                {
                    Id = СarApiContextFactory.BrandIdForUpdate,
                    Name = updateName,
                    Country = updateCountry,
                }, CancellationToken.None);
            
            // Assert
            var brand = await UnitOfWork.Brands.GetById(СarApiContextFactory.BrandIdForUpdate, CancellationToken.None);
            Assert.NotNull(brand);
            Assert.Equal(СarApiContextFactory.BrandIdForUpdate, brand.Id);
            Assert.Equal(updateName, brand.Name);
            Assert.True(brand.Country.Name.Equals(updateCountry.Name));
        }
        /// <summary>
        /// Проверяет успешное обновления марки автомобиля 
        /// с указанием ИД страны производителя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateBrandByCountryIdCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateBrandCommandHandler(UnitOfWork);
            var updateName = "Mastretta MXT";
            
            // Act
            await handler.Handle(
                new UpdateBrandCommand
                {
                    Id = СarApiContextFactory.BrandIdForUpdate,
                    Name = updateName,
                    CountryId = СarApiContextFactory.CountryIdForUpdate,
                }, CancellationToken.None);

            // Assert
            var brand = await UnitOfWork.Brands.GetById(СarApiContextFactory.BrandIdForUpdate, CancellationToken.None);
            Assert.NotNull(brand);
            Assert.Equal(СarApiContextFactory.BrandIdForUpdate, brand.Id);
            Assert.Equal(updateName, brand.Name);
            Assert.Equal(СarApiContextFactory.CountryIdForUpdate, brand.Country.Id);
            Assert.Equal("Korea", brand.Country.Name);
        }
        /// <summary>
        /// Проверяет появления исключения "NotFoundException"
        /// при обновлении марки автомобиля с ID, которого нет в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateBrandCommandHandler_FailWrongId()
        {
            // Arrange
            var handler = new UpdateBrandCommandHandler(UnitOfWork);

            // Act
            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () =>
            await handler.Handle(
                new UpdateBrandCommand
                {
                    Id = Guid.NewGuid(),
                },
                CancellationToken.None));
        }
    }
}
