using CarWebApi.CQRS.Commands.Cars;
using CarWebApi.Exceptions;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Cars.Commands
{
    /// <summary>
    /// Тестирование обработчика команд удаления автомобиля
    /// </summary>
    public class DeleteCarCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное удаление автомобиля по ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteCarCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteCarCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(
                new DeleteCarCommand
                {
                    Id = СarApiContextFactory.CarIdForDelete,
                },
                CancellationToken.None);

            // Assert
            Assert.Null(await UnitOfWork.Cars.GetById(СarApiContextFactory.CarIdForDelete, CancellationToken.None));
        }

        /// <summary>
        /// Проверяет появления исключения "NotFoundException"
        /// при удаление автомобиля с ID, которого нет в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteCarCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteCarCommandHandler(UnitOfWork);
            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCarCommand
                    {
                        Id = Guid.NewGuid(),
                    },
                    CancellationToken.None));
        }
    }
}
