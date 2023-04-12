using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.Exceptions;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Countries.Commands
{
    /// <summary>
    /// Тестирование обработчика команд удаления страны производителя
    /// </summary>
    public class DeleteCountryCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное удаление страны производителя по ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteCountryCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteCountryCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(
                new DeleteCountryCommand
                {
                    Id = СarApiContextFactory.CountryIdForDelete,
                },
                CancellationToken.None);

            // Assert
            Assert.Null(await UnitOfWork.Countries.GetById(СarApiContextFactory.CountryIdForDelete, CancellationToken.None));
        }

        /// <summary>
        /// Проверяет появления исключения "NotFoundException"
        /// при удаление страны производителя с ID, которого нет в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteCountryCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteCountryCommandHandler(UnitOfWork);
            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCountryCommand
                    {
                        Id = Guid.NewGuid(),
                    },
                    CancellationToken.None));
        }
    }
}
