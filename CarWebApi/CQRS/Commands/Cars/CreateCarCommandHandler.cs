using CarWebApi.Database;
using CarWebApi.Models.Cars;
using CarWebApi.Repositories.Interfaces;

namespace CarWebApi.CQRS.Commands.Cars
{
    /// <summary>
    /// Обработчик команды добавления автомобиля
    /// </summary>
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {
        /// <summary>
        /// Менеджер репозиториев
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// Конструктор обработчика команды добавления автомобиля
        /// </summary>
        /// <param name="unitOfWork">Менеджер репозиториев</param>
        public CreateCarCommandHandler(IUnitOfWork unitOfWork) =>
            UnitOfWork = unitOfWork;

        /// <summary>
        /// Логика обработки команды CreateCarCommand
        /// </summary>
        /// <param name="request">Ответ на команду</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>ИД автомобиля</returns>
        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = new Car
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Brand = request.Brand,
                Long = request.Long,
                Pow = request.Pow,
                Price = request.Price,
                CreationDate = DateTime.Now,
                ModifiedDate = null,
            };
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                try
                {
                    UnitOfWork.Cars.Add(car);
                    UnitOfWork.Save();
                    UnitOfWork.CommitTransaction(transaction);
                }
                catch (Exception ex)
                {
                    UnitOfWork.RollbackTransaction(transaction);
                    throw ex;
                }

                return car.Id;
            }
        }
    }
}
