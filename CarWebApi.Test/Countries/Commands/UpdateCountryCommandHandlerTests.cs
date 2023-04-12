using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.Exceptions;
using CarWebApi.Test.Common;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Test.Countries.Commands
{
    /// <summary>
    /// Тестирование обработчика команд обновления страны производителя
    /// </summary>
    public class UpdateCountryCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное обновления страны производителя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateCountryCommandHandler_Success()
        {
            // Arrange
            var handler = new UpdateCountryCommandHandler(UnitOfWork);
            var updateName = "Spain";

            // Act
            await handler.Handle(
                new UpdateCountryCommand
                {
                    Id = СarApiContextFactory.CountryIdForUpdate,
                    Name = updateName
                }, CancellationToken.None);
            // Assert
            var country = await UnitOfWork.Countries.GetById(СarApiContextFactory.CountryIdForUpdate, CancellationToken.None);
            Assert.NotNull(country);
            Assert.Equal(СarApiContextFactory.CountryIdForUpdate, country.Id);
            Assert.Equal(updateName, country.Name);
        }
        /// <summary>
        /// Проверяет появления исключения "NotFoundException"
        /// при обновлении страны производителя с ID, которого нет в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateCountryCommandHandler_FailWrongId()
        {
            // Arrange
            var handler = new UpdateCountryCommandHandler(UnitOfWork);

            // Act
            //Assert
            await Assert.ThrowsAnyAsync<NotFoundException>(async () =>
            await handler.Handle(
                new UpdateCountryCommand
                {
                    Id = Guid.NewGuid(),
                },
                CancellationToken.None));
        }
    }
}
