using CarWebApi.CQRS.Commands.Countries;
using CarWebApi.Test.Common;
using Microsoft.EntityFrameworkCore;

namespace CarWebApi.Test.Countries.Commands
{
    /// <summary>
    /// Тестирование обработчика команд создания страны производителя
    /// </summary>
    public class CreateCountryCommandHandlerTests : TestCommandBase
    {
        /// <summary>
        /// Проверяет успешное создание страны производителя
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateCountryCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateCountryCommandHandler(UnitOfWork);

            string countryName = "Korea";

            // Act
            var countryId = await handler.Handle(
                new CreateCountryCommand
                {
                    Name = countryName,
                },
                CancellationToken.None);

            var result = await UnitOfWork.Countries.GetById(countryId, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(countryName, result.Name);
        }

    }
}
