using CarWebApi.CQRS.Commands.Brands;
using CarWebApi.Exceptions;
using CarWebApi.Test.Common;

namespace CarWebApi.Test.Brands.Commands
{
    /// <summary>
    /// Тестирование обработчика команд удаления марки автомобиля
    /// </summary>
    public class DeleteBrandCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное удаление марки автомобиля по ID
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteBrandCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteBrandCommandHandler(UnitOfWork);

            // Act
            await handler.Handle(
                new DeleteBrandCommand
                {
                    Id = СarApiContextFactory.BrandIdForDelete,
                },
                CancellationToken.None);

            // Assert
            Assert.Null(await UnitOfWork.Brands.GetById(СarApiContextFactory.BrandIdForDelete, CancellationToken.None));
        }

        /// <summary>
        /// Проверяет появления исключения "NotFoundException"
        /// при удаление марки автомобиля с ID, которого нет в БД
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DeleteBrandCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteBrandCommandHandler(UnitOfWork);
            // Act

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteBrandCommand
                    {
                        Id = Guid.NewGuid(),
                    },
                    CancellationToken.None));
        }
    }
}
