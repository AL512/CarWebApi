using CarWebApi.Database;
using CarWebApi.Models.Countries;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Countries
{
    /// <summary>
    /// Обработчик команды добавления страны производителя
    /// </summary>
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, Guid>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Обработчик команды добавления автомобиля
        /// </summary>
        /// <param name="dbContext">Менеджер репозиториев</param>
        public CreateCountryCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>ИД страны производителя в БД</returns>
        public async Task<Guid> Handle(CreateCountryCommand request,
            CancellationToken cancellationToken)
        {
            var country = new Country
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreationDate = DateTime.Now,
            };

            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    UnitOfWork.Countries.Add(country);
                    UnitOfWork.Save();
                    UnitOfWork.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    UnitOfWork.RollbackTransaction(transaction);
                    throw ex;
                }
            }
            return country.Id;
        }
    }
}
